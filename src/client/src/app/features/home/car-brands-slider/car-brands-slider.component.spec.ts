import { ComponentFixture, TestBed } from "@angular/core/testing";

import { CarBrandsSliderComponent } from "./car-brands-slider.component";

describe("CarBrandsSliderComponent", () => {
  let component: CarBrandsSliderComponent;
  let fixture: ComponentFixture<CarBrandsSliderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CarBrandsSliderComponent],
    });
    fixture = TestBed.createComponent(CarBrandsSliderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
