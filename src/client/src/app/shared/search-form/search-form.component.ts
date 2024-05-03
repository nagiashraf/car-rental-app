import { Component, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { DateRange, MatCalendar } from "@angular/material/datepicker";
import {
  DefaultMatCalendarRangeStrategy,
  MAT_DATE_RANGE_SELECTION_STRATEGY,
} from "@angular/material/datepicker";
import { fadeAndSlideUp } from "./animations";
import { InputDropdownComponent } from "./input-dropdown/input-dropdown.component";
import { LocaleService } from "src/app/core/services/locale.service";
import { DateTime, Duration } from "luxon";
import { DatepickerHeaderComponent } from "./datepicker-header/datepicker-header.component";

@Component({
  selector: "app-search-form",
  templateUrl: "./search-form.component.html",
  styleUrls: ["./search-form.component.scss", "./shared.scss"],
  providers: [
    {
      provide: MAT_DATE_RANGE_SELECTION_STRATEGY,
      useClass: DefaultMatCalendarRangeStrategy,
    },
  ],
  animations: [fadeAndSlideUp],
})
export class SearchFormComponent {
  searchForm!: FormGroup;
  locationPanelMinWidth = 400;
  datePanelWidth = 300;
  dropoffAtDifferentLocation = false;
  pickupLocationsLoading = false;
  dropoffLocationsLoading = false;
  minDate = DateTime.now();
  maxDate = DateTime.now().plus({ years: 1 });
  initialPickupDate = DateTime.now().plus({ days: 5 });
  selectedPickupDate = this.initialPickupDate;
  selectedDropoffDateRange: DateRange<DateTime> = new DateRange(
    this.initialPickupDate,
    null,
  );
  datePickerHeader = DatepickerHeaderComponent;
  @ViewChild("pickupDateDropdown")
  pickupDateDropdown!: InputDropdownComponent;
  @ViewChild("dropoffDateDropdown")
  dropoffDateDropdown!: InputDropdownComponent;
  @ViewChild("pickupDateCalendar")
  pickupDateCalendar!: MatCalendar<DateTime>;
  @ViewChild("dropoffDateCalendar")
  dropoffDateCalendar!: MatCalendar<DateTime>;

  constructor(
    private fb: FormBuilder,
    private localeService: LocaleService,
  ) {
    this.initializeForm();
  }

  onSubmit(): void {}

  focusActivePickupDate(): void {
    this.pickupDateCalendar.activeDate = this.selectedPickupDate;
    this.pickupDateCalendar.focusActiveCell();
  }

  focusActiveDropoffDate(): void {
    this.selectedDropoffDateRange = new DateRange(
      this.selectedPickupDate,
      null,
    );

    this.dropoffDateCalendar.activeDate =
      this.selectedDropoffDateRange.start ?? DateTime.now();
    this.dropoffDateCalendar.focusActiveCell();
  }

  onPickupDateSelectedChange(date: DateTime): void {
    if (this.searchForm.get("dropoffDate")?.value < date) {
      this.searchForm.get("dropoffDate")?.setValue(date);
    }

    this.pickupDateDropdown.optionSelected.emit(date);
  }

  onDropoffDateRangeSelectedChange(date: DateTime): void {
    if (
      this.selectedDropoffDateRange &&
      this.selectedDropoffDateRange.start &&
      date > this.selectedDropoffDateRange.start &&
      !this.selectedDropoffDateRange.end
    ) {
      this.selectedDropoffDateRange = new DateRange(
        this.selectedDropoffDateRange.start,
        date,
      );
      this.searchForm.get("dropoffDate")?.setValue(date);
      this.dropoffDateDropdown.optionSelected.emit(date);
    } else {
      this.selectedPickupDate = date;
      this.searchForm.get("pickupDate")?.setValue(date);
      this.selectedDropoffDateRange = new DateRange(date, null);
    }
  }

  initializeForm(): void {
    const initialDropoffDate = DateTime.now().plus({ days: 8 });
    const initialTime = Duration.fromObject({ hours: 10 });

    this.searchForm = this.fb.group({
      pickupLocation: [null],
      dropoffLocation: [null],
      pickupDate: [this.selectedPickupDate],
      pickupTime: [initialTime],
      dropoffDate: [initialDropoffDate],
      dropoffTime: [initialTime],
    });
  }

  formatDate(date: DateTime): string {
    return date.toFormat("EEE dd LLL");
  }

  formatTime(time: Duration): string {
    return time.toFormat("hh:mm");
  }

  // getMaxDate(): Date {
  //   const maxDate = new Date();
  //   maxDate.setFullYear(maxDate.getFullYear() + 1);
  //   return maxDate;
  // }

  // getInitialTime(): Date {
  //   const initialPickupDate = this.initialPickupDate;
  //   initialPickupDate.setHours(10, 0);
  //   return initialPickupDate;
  // }

  // getInitialPickupDate(): Date {
  //   const defaultPickupDate = new Date();
  //   defaultPickupDate.setDate(defaultPickupDate.getDate() + 5);
  //   return defaultPickupDate;
  // }
  // getInitialDropoffDate(): DateTime {
  //   const defaultDropoffDate = DateTime.now().plus({ days: 8 });
  //   return defaultDropoffDate;
  // }

  setPickupLocationInputValue(value: string): void {
    // this.pickupLocationInputElement.nativeElement.focus();
    this.searchForm.get("pickupLocation")!.setValue(value);
    // this.showPickupLocationPanel = true;
  }
}
