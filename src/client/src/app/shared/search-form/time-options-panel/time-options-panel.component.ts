import { CdkOption, ListboxValueChangeEvent } from "@angular/cdk/listbox";
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  QueryList,
  ViewChildren,
} from "@angular/core";
import { Duration } from "luxon";

@Component({
  selector: "app-time-options-panel",
  templateUrl: "./time-options-panel.component.html",
  styleUrls: ["./time-options-panel.component.scss", "../shared.scss"],
})
export class TimeOptionsPanelComponent implements OnInit {
  timeOptions = this.getTimeOptions();
  selectedTimeOption!: Duration;

  @Input({ required: true })
  currentFormControlValue!: Duration;
  @Output()
  timeOptionSelected = new EventEmitter<Duration>();
  @ViewChildren(CdkOption)
  timeListboxOptions!: QueryList<CdkOption<Duration>>;

  ngOnInit(): void {
    this.selectedTimeOption = this.currentFormControlValue;
  }

  focusActiveTimeOption(): void {
    this.timeListboxOptions
      .filter(
        (option) =>
          option.value.as("seconds") === this.selectedTimeOption.as("seconds"),
      )[0]
      .focus();
  }

  onTimeOptionSelected(event: ListboxValueChangeEvent<Duration>): void {
    const selectedTime = event.value[0];
    this.selectedTimeOption = selectedTime;
    this.timeOptionSelected.emit(selectedTime);
  }

  getTimeOptions(): Duration[] {
    let timeOptions = [];
    for (let i = 0; i < 24; i++) {
      timeOptions.push(Duration.fromObject({ hours: i }));
      timeOptions.push(Duration.fromObject({ hours: i, minutes: 30 }));
    }
    return timeOptions;
  }

  compareTime(time1: Duration, time2: Duration) {
    return time1.as("seconds") === time2.as("seconds");
  }
}
