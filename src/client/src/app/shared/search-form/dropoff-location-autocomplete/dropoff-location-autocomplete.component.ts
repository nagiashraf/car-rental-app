import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  QueryList,
  ViewChild,
  ViewChildren,
} from "@angular/core";
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from "@angular/forms";
import { fadeAndSlideUp } from "../animations";
import { LocationSearchResult } from "src/app/core/models/location.model";
import { Observable, Subscription, of, tap } from "rxjs";
import { overlayConfig } from "../OverlayConfig";
import { ActiveDescendantKeyManager } from "@angular/cdk/a11y";
import { LocationOptionComponent } from "../location-option/location-option.component";
import { CdkConnectedOverlay, ViewportRuler } from "@angular/cdk/overlay";
import { LocationService } from "src/app/core/services/location.service";

@Component({
  selector: "app-dropoff-location-autocomplete",
  templateUrl: "./dropoff-location-autocomplete.component.html",
  styleUrls: [
    "./dropoff-location-autocomplete.component.scss",
    "../shared.scss",
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: DropoffLocationAutocompleteComponent,
    },
  ],
  animations: [fadeAndSlideUp],
})
export class DropoffLocationAutocompleteComponent
  implements OnInit, AfterViewInit, OnDestroy, ControlValueAccessor
{
  availableDropoffLocations: LocationSearchResult[] = [];
  filteredLocations: LocationSearchResult[] = [];
  pickupLocation: LocationSearchResult | undefined;
  loading = false;
  showPanel = false;
  overlayConfig = overlayConfig;
  panelKeyManager!: ActiveDescendantKeyManager<LocationOptionComponent>;
  onChange = (value: any) => {};
  onTouched = () => {};
  isDisabled = false;
  pickupLocationSubscription!: Subscription;
  dropoffLocationsSubscription!: Subscription;
  viewportRulerSubscription!: Subscription;

  @Input({ required: true })
  pickupLocation$!: Observable<LocationSearchResult>;
  @Input({ required: true })
  currentlySelectedPickupLocation!: LocationSearchResult | null;
  @Input()
  panelWidth: number | undefined;
  @Input()
  panelMinWidth: number | undefined;
  @Output()
  loadingStateChanged = new EventEmitter<boolean>();
  @ViewChild("inputElement", { static: true })
  inputElement!: ElementRef<HTMLInputElement>;
  @ViewChild("overlay", { read: CdkConnectedOverlay })
  overlay!: CdkConnectedOverlay;
  @ViewChildren(LocationOptionComponent)
  locationOptions!: QueryList<LocationOptionComponent>;

  constructor(
    private locationService: LocationService,
    private viewportRuler: ViewportRuler,
  ) {}

  ngOnInit(): void {
    if (this.currentlySelectedPickupLocation) {
      this.pickupLocation = this.currentlySelectedPickupLocation;
    }
    this.subscribeToPickupLocation();
    this.subscribeToViewportRuler();
  }

  ngAfterViewInit(): void {
    if (!this.panelMinWidth) {
      this.panelMinWidth = this.getInputElementWidth();
    }
    this.panelKeyManager = new ActiveDescendantKeyManager(
      this.locationOptions,
    ).withWrap();
  }

  ngOnDestroy(): void {
    if (this.pickupLocationSubscription) {
      this.pickupLocationSubscription.unsubscribe();
    }

    if (this.dropoffLocationsSubscription) {
      this.dropoffLocationsSubscription.unsubscribe();
    }

    if (this.viewportRulerSubscription) {
      this.viewportRulerSubscription.unsubscribe();
    }
  }

  onFocus(): void {
    if (this.pickupLocation && this.availableDropoffLocations.length < 1) {
      this.dropoffLocationsSubscription = this.locationService
        .getDropoffLocations(
          this.pickupLocation.id,
          this.pickupLocation.languageId,
        )
        .pipe(tap(() => this.updateLoading(true)))
        .subscribe((data) => {
          this.updateLoading(false);
          this.availableDropoffLocations = data;
          this.filteredLocations = this.availableDropoffLocations;
        });
    }

    this.showPanel = true;
  }

  onInput(value: string): void {
    if (this.availableDropoffLocations.length > 0) {
      this.filter(value);
    }
  }

  onKeydown(event: KeyboardEvent): void {
    switch (event.key) {
      case "Escape":
      case "Tab":
        this.showPanel = false;
        break;
      case "Enter":
        if (this.panelKeyManager.activeItem) {
          this.selectOption(this.panelKeyManager.activeItem?.location);
        }
        break;
      default:
        this.panelKeyManager.onKeydown(event);
    }
  }

  onOverlayOutsideClick({ target }: MouseEvent): void {
    if (target !== this.inputElement.nativeElement) {
      const activeItem = this.panelKeyManager.activeItem;
      if (activeItem) {
        this.selectOption(activeItem?.location);
      } else {
        this.showPanel = false;
      }
    }
  }

  selectOption(location: LocationSearchResult): void {
    this.onChange(location);
    this.showPanel = false;
    this.inputElement.nativeElement.value = this.getDisplayText(location);
  }

  writeValue(location: any): void {
    this.inputElement.nativeElement.value = this.getDisplayText(location);
  }

  registerOnChange(fn: (value: any) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  private getInputElementWidth(): number {
    return this.inputElement.nativeElement.getBoundingClientRect().width;
  }

  private getDisplayText(location: LocationSearchResult): string {
    return location
      ? `${location.name}, ${location.city}, ${location.country}`
      : ``;
  }

  private updateLoading(value: boolean): void {
    this.loading = value;
    this.loadingStateChanged.emit(value);
  }

  private filter(value: string): void {
    const filterValue = value.toLowerCase();

    this.filteredLocations = this.availableDropoffLocations.filter(
      (location) =>
        location.name.toLowerCase().includes(filterValue) ||
        location.city.toLowerCase().includes(filterValue) ||
        location.country.toLowerCase().includes(filterValue),
    );
  }

  private subscribeToPickupLocation(): void {
    this.pickupLocationSubscription = this.pickupLocation$.subscribe((data) => {
      this.pickupLocation = data;
      this.availableDropoffLocations = [];
      this.filteredLocations = [];
      this.inputElement.nativeElement.value = "";
      this.onChange(null);
    });
  }

  private subscribeToViewportRuler(): void {
    this.viewportRulerSubscription = this.viewportRuler
      .change()
      .subscribe(() => {
        if (this.overlay && this.showPanel) {
          this.overlay.overlayRef.updateSize({
            width: this.getInputElementWidth(),
          });
        }
      });
  }
}
