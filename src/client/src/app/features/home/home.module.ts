import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HomeComponent } from "./home.component";

import { CdkAccordionModule } from "@angular/cdk/accordion";
import { CarBrandsSliderComponent } from "./car-brands-slider/car-brands-slider.component";
import { BenefitComponent } from "./benefit/benefit.component";
import { SharedModule } from "src/app/shared/shared.module";
import { DestinationCardComponent } from "./destination-card/destination-card.component";
import { FaqAccordionComponent } from "./faq-accordion/faq-accordion.component";

@NgModule({
  declarations: [
    HomeComponent,
    CarBrandsSliderComponent,
    BenefitComponent,
    DestinationCardComponent,
    FaqAccordionComponent,
  ],
  imports: [CommonModule, SharedModule, CdkAccordionModule],
  exports: [HomeComponent],
})
export class HomeModule {}
