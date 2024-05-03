import { ComponentFixture, TestBed } from "@angular/core/testing";
import { Component, NO_ERRORS_SCHEMA } from "@angular/core";
import { HomeComponent } from "./home.component";
import { ViewportScroller } from "@angular/common";
import { SearchFormComponent } from "src/app/shared/search-form/search-form.component";
import { By } from "@angular/platform-browser";
import { click } from "src/testing-utilities";

@Component({
  selector: "app-search-form",
  template: "",
  providers: [
    { provide: SearchFormComponent, useClass: SearchFormStubComponent },
  ],
})
class SearchFormStubComponent {
  setPickupLocationInputValue() {}
}

describe("HomeComponent", () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let viewportScrollerSpy: jasmine.SpyObj<ViewportScroller>;

  beforeEach(() => {
    viewportScrollerSpy = jasmine.createSpyObj("ViewportScroller", [
      "scrollToPosition",
    ]);

    TestBed.configureTestingModule({
      declarations: [HomeComponent, SearchFormStubComponent],
      providers: [{ provide: ViewportScroller, useValue: viewportScrollerSpy }],
      schemas: [NO_ERRORS_SCHEMA],
    });
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should set pickup location input value and scroll to top when setPickupLocationInputValue is called", () => {
    const city = "Test City";
    spyOn(component.searchForm, "setPickupLocationInputValue");

    component.setPickupLocationInputValue(city);

    expect(
      component.searchForm.setPickupLocationInputValue,
    ).toHaveBeenCalledWith(city);
    expect(viewportScrollerSpy.scrollToPosition).toHaveBeenCalledWith([0, 0]);
  });

  it("should scroll to top when call to action button is clicked", () => {
    const button = fixture.debugElement.query(By.css("button"));
    spyOn(component.searchForm, "setPickupLocationInputValue");

    click(button);

    expect(
      component.searchForm.setPickupLocationInputValue,
    ).toHaveBeenCalledWith("");
    expect(viewportScrollerSpy.scrollToPosition).toHaveBeenCalledWith([0, 0]);
  });

  it("should render benefit sections", () => {
    const benefits = fixture.debugElement.queryAll(By.css("app-benefit"));
    expect(benefits.length).toBeGreaterThan(0);
  });

  it("should render destination cards", () => {
    const destinationCards = fixture.debugElement.queryAll(
      By.css("app-destination-card"),
    );
    expect(destinationCards.length).toBeGreaterThan(0);
  });
});
