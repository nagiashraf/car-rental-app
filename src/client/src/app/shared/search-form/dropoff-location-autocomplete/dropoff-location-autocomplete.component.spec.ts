import { ComponentFixture, TestBed } from "@angular/core/testing";

import { DropoffLocationAutocompleteComponent } from "./dropoff-location-autocomplete.component";

describe("DropoffLocationAutocompleteComponent", () => {
  let component: DropoffLocationAutocompleteComponent;
  let fixture: ComponentFixture<DropoffLocationAutocompleteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DropoffLocationAutocompleteComponent],
    });
    fixture = TestBed.createComponent(DropoffLocationAutocompleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
