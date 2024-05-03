import { Highlightable } from "@angular/cdk/a11y";
import { Component, ElementRef, HostBinding, Input } from "@angular/core";
import { LocationSearchResult } from "src/app/core/models/location.model";

@Component({
  selector: "app-location-option",
  templateUrl: "./location-option.component.html",
  styleUrls: ["./location-option.component.scss"],
})
export class LocationOptionComponent implements Highlightable {
  private _isActive = false;

  @Input({ required: true }) location!: LocationSearchResult;

  constructor(private _element: ElementRef<HTMLElement>) {}

  @HostBinding("class.active") get isActive() {
    return this._isActive;
  }

  setActiveStyles(): void {
    this._isActive = true;
    this._element.nativeElement.scrollIntoView({ block: "nearest" });
  }

  setInactiveStyles(): void {
    this._isActive = false;
  }
}
