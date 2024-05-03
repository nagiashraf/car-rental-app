import {
  Directive,
  ElementRef,
  HostListener,
  Input,
  OnInit,
  Renderer2,
} from "@angular/core";

@Directive({
  selector: "[appFloatingLabel]",
})
export class FloatingLabelDirective implements OnInit {
  @Input() appFloatingLabel = "";
  labelEl!: ElementRef;

  constructor(
    private el: ElementRef,
    private renderer: Renderer2,
  ) {}
  ngOnInit(): void {
    this.labelEl = this.renderer.createElement("label");
    const text = this.renderer.createText(this.appFloatingLabel);
    this.renderer.appendChild(this.labelEl, text);
    this.renderer.appendChild(this.el.nativeElement.parentNode, this.labelEl);

    if (!this.el.nativeElement.parentNode.classList.contains("relative")) {
      this.renderer.addClass(this.el.nativeElement.parentNode, "relative");
    }

    const inputLeftPadding = window
      .getComputedStyle(this.el.nativeElement)
      .getPropertyValue("padding-left");
    this.renderer.setStyle(this.labelEl, "left", inputLeftPadding);

    this.renderer.addClass(this.labelEl, "absolute");
    this.renderer.addClass(this.labelEl, "-translate-y-1/2");
    this.renderer.addClass(this.labelEl, "pointer-events-none");
    this.renderer.addClass(this.labelEl, "transition-all");
    this.renderer.addClass(this.labelEl, "duration-300");
    this.renderer.addClass(this.labelEl, "ease-out");

    setTimeout(() => {
      if (!this.el.nativeElement.value) {
        this.renderer.addClass(this.labelEl, "top-1/2");
        this.renderer.addClass(this.labelEl, "text-base");
        this.renderer.addClass(this.labelEl, "text-gray-400");
      } else {
        this.renderer.addClass(this.labelEl, "top-3");
        this.renderer.addClass(this.labelEl, "text-xs");
        this.renderer.addClass(this.labelEl, "text-gray-600");
      }
    });

    const inputId = this.el.nativeElement.id;
    this.renderer.setAttribute(this.labelEl, "for", inputId);
  }

  @HostListener("focus") onFocus() {
    this.renderer.removeClass(this.labelEl, "top-1/2");
    this.renderer.removeClass(this.labelEl, "text-base");
    this.renderer.removeClass(this.labelEl, "text-gray-400");

    this.renderer.addClass(this.labelEl, "top-3");
    this.renderer.addClass(this.labelEl, "text-xs");
    this.renderer.addClass(this.labelEl, "text-gray-600");
  }

  @HostListener("blur") onBlur() {
    if (!this.el.nativeElement.value) {
      this.renderer.removeClass(this.labelEl, "top-3");
      this.renderer.removeClass(this.labelEl, "text-xs");
      this.renderer.removeClass(this.labelEl, "text-gray-600");

      this.renderer.addClass(this.labelEl, "top-1/2");
      this.renderer.addClass(this.labelEl, "text-base");
      this.renderer.addClass(this.labelEl, "text-gray-400");
    }
  }
}
