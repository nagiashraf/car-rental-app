import { ComponentFixture, TestBed } from "@angular/core/testing";

import { DatepickerHeaderComponent } from "./datepicker-header.component";

describe("DatepickerHeaderComponent", () => {
  let component: DatepickerHeaderComponent;
  let fixture: ComponentFixture<DatepickerHeaderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DatepickerHeaderComponent],
    });
    fixture = TestBed.createComponent(DatepickerHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
