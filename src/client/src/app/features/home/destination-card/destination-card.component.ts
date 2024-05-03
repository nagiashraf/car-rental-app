import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
  selector: "app-destination-card",
  templateUrl: "./destination-card.component.html",
  styleUrls: ["./destination-card.component.scss"],
})
export class DestinationCardComponent {
  @Input({ required: true }) destination!: { city: string; imgPath: string };
  @Output() cardClicked = new EventEmitter<string>();

  setPickupLocationInputValue(city: string): void {
    this.cardClicked.emit(city);
  }
}
