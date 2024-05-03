import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HeaderComponent } from "./header/header.component";
import { FooterComponent } from "./footer/footer.component";
import { SearchFormComponent } from "./search-form/search-form.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { OverlayModule } from "@angular/cdk/overlay";
import { A11yModule } from "@angular/cdk/a11y";
import { CdkListboxModule } from "@angular/cdk/listbox";
import { DialogModule } from "@angular/cdk/dialog";
import { LocationOptionComponent } from "./search-form/location-option/location-option.component";
import { FloatingLabelDirective } from "./search-form/directives/floating-label/floating-label.directive";
import { PickupLocationAutocompleteComponent } from "./search-form/pickup-location-autocomplete/pickup-location-autocomplete.component";
import { TimeOptionsPanelComponent } from "./search-form/time-options-panel/time-options-panel.component";
import { DropdownTriggerComponent } from "./header/dropdown-trigger/dropdown-trigger.component";
import { CurrencyDialogComponent } from "./header/currency-dialog/currency-dialog.component";
import { LanguagesPanelComponent } from "./header/languages-panel/languages-panel.component";
import { DropoffLocationAutocompleteComponent } from "./search-form/dropoff-location-autocomplete/dropoff-location-autocomplete.component";
import { InputDropdownComponent } from "./search-form/input-dropdown/input-dropdown.component";
import { DropdownDirective } from "./search-form/input-dropdown/dropdown.directive";
import { MatLuxonDateModule } from "@angular/material-luxon-adapter";
import { DatepickerHeaderComponent } from "./search-form/datepicker-header/datepicker-header.component";

@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    SearchFormComponent,
    LocationOptionComponent,
    FloatingLabelDirective,
    PickupLocationAutocompleteComponent,
    TimeOptionsPanelComponent,
    DropdownTriggerComponent,
    CurrencyDialogComponent,
    LanguagesPanelComponent,
    DropoffLocationAutocompleteComponent,
    InputDropdownComponent,
    DropdownDirective,
    DatepickerHeaderComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    OverlayModule,
    A11yModule,
    CdkListboxModule,
    DialogModule,
    MatDatepickerModule,
    MatLuxonDateModule,
    MatProgressSpinnerModule,
  ],
  exports: [HeaderComponent, FooterComponent, SearchFormComponent],
})
export class SharedModule {}
