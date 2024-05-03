import { ActiveDescendantKeyManager } from "@angular/cdk/a11y";
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
import { LocationOptionComponent } from "../location-option/location-option.component";
import { overlayConfig } from "../OverlayConfig";
import {
  Subscription,
  debounceTime,
  fromEvent,
  map,
  switchMap,
  tap,
} from "rxjs";
import { LocationSearchResult } from "src/app/core/models/location.model";
import { fadeAndSlideUp } from "../animations";
import { LocationService } from "src/app/core/services/location.service";
import { ViewportRuler } from "@angular/cdk/scrolling";
import { CdkConnectedOverlay } from "@angular/cdk/overlay";

@Component({
  selector: "app-pickup-location-autocomplete",
  templateUrl: "./pickup-location-autocomplete.component.html",
  styleUrls: [
    "./pickup-location-autocomplete.component.scss",
    "../shared.scss",
  ],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: PickupLocationAutocompleteComponent,
    },
  ],
  animations: [fadeAndSlideUp],
})
export class PickupLocationAutocompleteComponent
  implements OnInit, AfterViewInit, OnDestroy, ControlValueAccessor
{
  searchResultLocations: LocationSearchResult[] = [];
  maxSearchResultsNumber = 5;
  loading = false;
  showPanel = false;
  overlayConfig = overlayConfig;
  panelKeyManager!: ActiveDescendantKeyManager<LocationOptionComponent>;
  onChange = (value: any) => {};
  onTouched = () => {};
  isDisabled = false;
  inputEventSubscription!: Subscription;
  viewportRulerSubscription!: Subscription;

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
    this.subscribeToViewportRuler();
  }

  ngAfterViewInit(): void {
    this.subscribeToInputEvent();
    if (!this.panelMinWidth) {
      this.panelMinWidth = this.getInputElementWidth();
    }
    this.panelKeyManager = new ActiveDescendantKeyManager(
      this.locationOptions,
    ).withWrap();
  }

  ngOnDestroy(): void {
    if (this.inputEventSubscription) {
      this.inputEventSubscription.unsubscribe();
    }

    if (this.viewportRulerSubscription) {
      this.viewportRulerSubscription.unsubscribe();
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

  private subscribeToInputEvent(): void {
    this.inputEventSubscription = fromEvent(
      this.inputElement.nativeElement,
      "input",
    )
      .pipe(
        map((event) => (event.target as HTMLInputElement).value),
        tap((value) => {
          if (value.length > 0) {
            this.updateLoading(true);
          }
        }),
        debounceTime(300),
        switchMap((value) =>
          this.locationService.searchLocations(
            value,
            this.maxSearchResultsNumber,
          ),
        ),
      )
      .subscribe((data) => {
        this.updateLoading(false);
        this.searchResultLocations = data;
        if (this.inputElement.nativeElement.value) {
          if (!this.panelWidth) {
            this.panelWidth = this.getInputElementWidth();
          }
          this.showPanel = true;
          if (this.panelKeyManager.activeItemIndex !== 0) {
            setTimeout(() => {
              this.panelKeyManager.setFirstItemActive();
            });
          }
        } else {
          this.showPanel = false;
        }
      });
  }
}
