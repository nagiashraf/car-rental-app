import { Component, Input } from "@angular/core";

@Component({
  selector: "app-benefit",
  templateUrl: "./benefit.component.html",
  styleUrls: ["./benefit.component.scss"],
})
export class BenefitComponent {
  @Input({ required: true }) hText!: string;
  @Input({ required: true }) pText!: string;
}
