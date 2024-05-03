import { Component, ViewChild } from "@angular/core";
import { ViewportScroller } from "@angular/common";
import { SearchFormComponent } from "src/app/shared/search-form/search-form.component";

/**
 * This component represents the home page of the application.
 * It includes sections for introductory content, car brand slider, benefits,
 * top worldwide destinations, call to action, and frequently asked questions.
 */
@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent {
  /** An array of objects representing benefits header and paragraph text */
  benefits: { hText: string; pText: string }[] = this.getBenefitsText();

  /** An array of objects representing destination cities names and images */
  destinations: { city: string; imgPath: string }[] = this.getDestinations();

  /** Reference to the SearchFormComponent */
  @ViewChild(SearchFormComponent)
  searchForm!: SearchFormComponent;

  /**
   * Constructor
   * @param scroller - ViewportScroller for scrolling to the top of the page
   */
  constructor(private scroller: ViewportScroller) {}

  /**
   * Set the value of pickup location input in the search form.
   * Scrolls to the top of the page and focuses on the input afterward.
   *
   * @param city - The city name to set as the pickup location
   */
  setPickupLocationInputValue(city: string): void {
    this.searchForm.setPickupLocationInputValue(city);
    this.scroller.scrollToPosition([0, 0]);
  }

  private getBenefitsText(): { hText: string; pText: string }[] {
    return [
      {
        hText: $localize`:benefit heading|competitive prices:Competitive prices`,
        pText: $localize`:benefit paragraph|best value for money:Get the best value for your money with our affordable and transparent rates.`,
      },
      {
        hText: $localize`:benefit heading|wide selection of cars:Wide selection of cars`,
        pText: $localize`:benefit paragraph|variety of models and brands:Choose from a variety of models and brands to suit your needs and preferences.`,
      },
      {
        hText: $localize`:benefit heading|convenient locations:Convenient locations`,
        pText: $localize`:benefit paragraph|pick up and drop off across countries:Pick up and drop off your car at any of our locations across the countries.`,
      },
      {
        hText: $localize`:benefit heading|exceptional customer support:Exceptional customer support`,
        pText: $localize`:benefit paragraph|friendly and professional staff:Our friendly and professional staff are always ready to assist you with any questions or issues.`,
      },
    ];
  }

  private getDestinations(): { city: string; imgPath: string }[] {
    const baseImgPath = "/assets/images/destinations/";
    return [
      {
        city: $localize`:destination city|:Dubai`,
        imgPath: baseImgPath + "dubai.jpg",
      },
      {
        city: $localize`:destination city|:Cairo`,
        imgPath: baseImgPath + "cairo.jpg",
      },
      {
        city: $localize`:destination city|:New York`,
        imgPath: baseImgPath + "new-york.jpg",
      },
      {
        city: $localize`:destination city|:Rio de Janeiro`,
        imgPath: baseImgPath + "rio-de-janeiro.jpg",
      },
      {
        city: $localize`:destination city|:Barcelona`,
        imgPath: baseImgPath + "barcelona.jpg",
      },
      {
        city: $localize`:destination city|:Paris`,
        imgPath: baseImgPath + "paris.jpg",
      },
      {
        city: $localize`:destination city|:Rome`,
        imgPath: baseImgPath + "rome.jpg",
      },
      {
        city: $localize`:destination city|:London`,
        imgPath: baseImgPath + "london.jpg",
      },
    ];
  }
}
