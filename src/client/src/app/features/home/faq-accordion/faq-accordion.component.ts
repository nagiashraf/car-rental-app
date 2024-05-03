import { Component } from "@angular/core";
import { expand, indicatorFlip } from "../animations";

@Component({
  selector: "app-faq-accordion",
  templateUrl: "./faq-accordion.component.html",
  styleUrls: ["./faq-accordion.component.scss"],
  animations: [indicatorFlip, expand],
})
export class FaqAccordionComponent {
  faqs = this.getFaqs();

  getFaqs(): { question: string; answer: string }[] {
    return [
      {
        question: $localize`:faq question|age requirement:How old do I need to be to rent a car?`,
        answer: $localize`:faq answer|age requirement:The minimum age requirement for renting a car varies by location and car rental company. Typically, it's between 21 and 25 years old. Additional fees or restrictions may apply to renters under 25.`,
      },
      {
        question: $localize`:faq question|documents:What documents do I need to rent a car?`,
        answer: $localize`:faq answer|documents:You'll typically need a valid driver's license, a credit card in the renter's name for payment and deposit, and an additional form of identification like a passport.`,
      },
      {
        question: $localize`:faq question|additional driver:Can I add an additional driver to my rental agreement?`,
        answer: $localize`:faq answer|additional driver:Yes, additional drivers can usually be added to the rental agreement for a fee. They'll need to meet the same age and documentation requirements as the primary driver.`,
      },
      {
        question: $localize`:faq question|insurance:What insurance options are available?`,
        answer: $localize`:faq answer|insurance:We offer various insurance options including Collision Damage Waiver (CDW), Loss Damage Waiver (LDW), Personal Accident Insurance (PAI), and Supplemental Liability Insurance (SLI). These options vary by location and may be subject to terms and conditions.`,
      },
      {
        question: $localize`:faq question|fuel policy:What's the fuel policy?`,
        answer: $localize`:faq answer|fuel policy:Our cars are typically rented with a full tank and should be returned with a full tank. You can choose to prepay for fuel at the time of rental or refill the tank yourself before returning the car.`,
      },
      {
        question: $localize`:faq question|reservation modification:Can I modify or cancel my reservation?`,
        answer: $localize`:faq answer|reservation modification:Yes, reservations can usually be modified or canceled online or by contacting our customer service, subject to the terms of your booking and any applicable fees.`,
      },
      {
        question: $localize`:faq question|extras:Do you offer child seats or other extras?`,
        answer: $localize`:faq answer|extras:Yes, we offer a range of extras including child seats, GPS devices, ski racks, and more. These can be selected during the booking process or added at the rental counter.`,
      },
      {
        question: $localize`:faq question|mileage limit:Is there a mileage limit?`,
        answer: $localize`:faq answer|mileage limit:Most rentals come with unlimited mileage, but some promotions or special rates may have mileage restrictions. Please check your reservation details for specific information.`,
      },
      {
        question: $localize`:faq question|different location:Can I return the car to a different location?`,
        answer: $localize`:faq answer|different location:That depends on the pickup location you choose. In most cases, we offer one-way rentals allowing you to return the car to a different location. Additional fees may apply based on the drop-off location.`,
      },
      {
        question: $localize`:faq question|emergency:What do I do in case of an emergency or breakdown?`,
        answer: $localize`:faq answer|emergency:In case of an emergency or breakdown, please contact our 24/7 roadside assistance hotline provided on your rental agreement. We'll provide necessary assistance promptly.`,
      },
    ];
  }
}
