import {
  ConnectedPosition,
  Overlay,
  OverlayRef,
  ViewportRuler,
} from "@angular/cdk/overlay";
import { TemplatePortal } from "@angular/cdk/portal";
import {
  Directive,
  ElementRef,
  HostListener,
  Input,
  OnDestroy,
  OnInit,
  ViewContainerRef,
  forwardRef,
} from "@angular/core";
import { Subscription } from "rxjs";
import { InputDropdownComponent } from "./input-dropdown.component";
import { overlayConfig } from "../OverlayConfig";
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl } from "@angular/forms";

@Directive({
  selector: "[appDropdown]",
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: DropdownDirective,
      multi: true,
    }
  ]
})
export class DropdownDirective
  implements OnInit, OnDestroy, ControlValueAccessor
{
  overlayRef!: OverlayRef;
  templatePortal!: TemplatePortal;
  onChange: (value: any) => void = () => {};
  onTouched = () => {};
  viewportRulerSubscription = Subscription.EMPTY;

  @Input("appDropdown")
  dropdown!: InputDropdownComponent;

  constructor(
    private overlay: Overlay,
    private viewContainerRef: ViewContainerRef,
    private host: ElementRef<HTMLInputElement>,
    // private control: NgControl,
    private viewportRuler: ViewportRuler,
  ) {}

  ngOnInit(): void {
    this.host.nativeElement.setAttribute("readonly", "readonly");
    this.host.nativeElement.setAttribute("autocomplete", "off");
    this.host.nativeElement.style.cursor = "pointer";

    setTimeout(() => {
      // if (this.control.value && this.dropdown.displayValue) {
      //   this.host.nativeElement.value = this.dropdown.displayValue(
      //     this.control.value,
      //   );
      // }
    });

    this.dropdown.optionSelected.subscribe((value: any) => {
      this.closeDropdown();
      if (!this.dropdown.displayValue) {
        this.host.nativeElement.value = value;
      } else {
        this.host.nativeElement.value = this.dropdown.displayValue(value);
      }
      this.onChange(value);
    });
  }

  ngOnDestroy(): void {
    this.viewportRulerSubscription.unsubscribe();
    if (this.overlayRef) {
      this.overlayRef.dispose();
    }
  }

  @HostListener("click", ["$event"]) onClick(event: MouseEvent): void {
    this.openDropdown();
  }

  @HostListener("keydown", ["$event"]) onKeydown(event: KeyboardEvent): void {
    if (
      event.key === "Enter" ||
      event.key === " " ||
      event.key === "ArrowDown" ||
      event.key === "ArrowUp"
    ) {
      event.preventDefault();
      this.openDropdown();
    }
  }

  openDropdown(): void {
    // if (!this.host.nativeElement.value) {
    //   this.host.nativeElement.value = " "; // A workaround to float the label
    // }

    if (!this.overlayRef) {
      this.overlayRef = this.overlay.create({
        scrollStrategy: this.overlay.scrollStrategies.reposition(),
        positionStrategy: this.overlay
          .position()
          .flexibleConnectedTo(this.host)
          .withPositions(this.getOverlayPositions())
          .withDefaultOffsetY(overlayConfig.offsetY)
          .withFlexibleDimensions(overlayConfig.flexibleDimensions)
          .withPush(overlayConfig.push)
          .withViewportMargin(overlayConfig.viewportMargin)
          .withLockedPosition(overlayConfig.lockPosition),
        width: this.getDropdownWidth(),
        minWidth: this.getDropdownMinWidth(),
      });

      this.templatePortal = new TemplatePortal(
        this.dropdown.templateRef,
        this.viewContainerRef,
      );
      this.overlayRef.attach(this.templatePortal);

      this.overlayRef.outsidePointerEvents().subscribe((event: MouseEvent) => {
        event.stopPropagation(); // To prevent the input click event from being triggered, so the dropdown won't open again
        this.closeDropdown(false);
      });

      this.overlayRef.keydownEvents().subscribe((event: KeyboardEvent) => {
        switch (event.key) {
          case "Escape":
            // case "Tab":
            this.closeDropdown();
            break;
          case "Enter":
            // this.ngControl.control?.setValue(
            //   this.panel.keyManager.activeItem?.value,
            // );
            // this.closeDropdown();
            break;
          default:
          // this.panel.keyManager.onKeydown(event);
        }
      });

      // this.optionsClickSubscription = this.panel.optionsClick$.subscribe(
      //   (value) => {
      //     this.ngControl.control?.setValue(value);
      //     this.closeDropdown();
      //   },
      // );

      this.viewportRulerSubscription = this.viewportRuler
        .change()
        .subscribe(() => {
          if (this.overlayRef && this.overlayRef.hasAttached()) {
            this.overlayRef.updateSize({
              width: this.dropdown.width || this.getDropdownWidth(),
              minWidth: this.getDropdownMinWidth(),
            });
          }
        });
    }

    if (!this.overlayRef.hasAttached()) {
      this.overlayRef.attach(this.templatePortal);
    }

    this.dropdown.focusActiveOption.emit();
  }

  closeDropdown(focusHostElement = true): void {
    if (this.overlayRef && this.overlayRef.hasAttached()) {
      this.overlayRef.detach();
      // this.host.nativeElement.value = "";
      if (focusHostElement) {
        this.host.nativeElement.focus();
      }
    }
  }

  writeValue(value: any): void {
    setTimeout(() => {
      this.host.nativeElement.value = this.dropdown.displayValue
      ? this.dropdown.displayValue(value)
      : value;
    });
  }

  registerOnChange(fn: (value: any) => {}): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => {}) {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean) {
    this.host.nativeElement.disabled = isDisabled;
  }

  private getOverlayPositions(): ConnectedPosition[] {
    return [
      {
        originX: "start",
        originY: "bottom",
        overlayX: "start",
        overlayY: "top",
      },
      // {
      //   originX: "start",
      //   originY: "top",
      //   overlayX: "start",
      //   overlayY: "bottom",
      // },
      // {
      //   originX: "end",
      //   originY: "top",
      //   overlayX: "end",
      //   overlayY: "bottom",
      // },
      // {
      //   originX: "end",
      //   originY: "bottom",
      //   overlayX: "end",
      //   overlayY: "top",
      // },
    ];
  }

  private getDropdownWidth(): number {
    const hostWidth = this.host.nativeElement.getBoundingClientRect().width;
    return this.dropdown.width || hostWidth;
  }

  private getDropdownMinWidth(): number {
    const hostWidth = this.host.nativeElement.getBoundingClientRect().width;
    return this.dropdown.minWidth || hostWidth;
  }
}
