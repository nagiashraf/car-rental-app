import { Component } from "@angular/core";

@Component({
  selector: "app-car-brands-slider",
  templateUrl: "./car-brands-slider.component.html",
  styleUrls: ["./car-brands-slider.component.scss"],
})
export class CarBrandsSliderComponent {
  images = [
    "/assets/images/car-brands/Mercedes.png",
    "/assets/images/car-brands/Audi.png",
    "/assets/images/car-brands/Ford.png",
    "/assets/images/car-brands/Toyota.png",
    "/assets/images/car-brands/BMW.png",
    "/assets/images/car-brands/Chevrolet.png",
    "/assets/images/car-brands/Hyundai.png",
    "/assets/images/car-brands/Mazda.png",
    "/assets/images/car-brands/Fiat.png",
    "/assets/images/car-brands/Jeep.png",
    "/assets/images/car-brands/Nissan.png",
    "/assets/images/car-brands/Volkswagen.png",
    "/assets/images/car-brands/Mitsubishi.png",
    "/assets/images/car-brands/Jaguar.png",
    "/assets/images/car-brands/Opel.png",
    "/assets/images/car-brands/KIA.png",
    "/assets/images/car-brands/Seat.png",
  ];

  constructor() {
    this.images = [...this.images, ...this.images];
  }
}
