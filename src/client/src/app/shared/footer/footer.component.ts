import { Component } from "@angular/core";

@Component({
  selector: "app-footer",
  templateUrl: "./footer.component.html",
  styleUrls: ["./footer.component.css"],
})
export class FooterComponent {
  email = "nagiashraf50@gmail.com";
  currentYear = new Date().getFullYear();
}
