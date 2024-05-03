import { ChangeDetectorRef, Component, Inject, OnDestroy } from "@angular/core";
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MatDateFormats,
} from "@angular/material/core";
import { MatCalendar } from "@angular/material/datepicker";
import { Subject, takeUntil } from "rxjs";

@Component({
  selector: "app-datepicker-header",
  template: `
    <div class="flex justify-between items-center p-2">
      <button [style.visibility]="prevIsDisabled ? 'hidden' : 'visible'" class="icon-wrapper" (click)="prevClicked()">
        <span class="material-symbols-sharp icon">keyboard_arrow_left</span>
      </button>
      <span class="font-bold text-sm">{{ periodLabel }}</span>
      <button [style.visibility]="nextIsDisabled ? 'hidden' : 'visible'" class="icon-wrapper" (click)="nextClicked()">
        <span class="material-symbols-sharp icon">keyboard_arrow_right</span>
      </button>
    </div>
  `,
  styles: [`
  .icon-wrapper {
    @apply flex justify-center items-center w-8 h-8 rounded-full transition-colors duration-200 hover:bg-off-white;
  }

  .icon {
    @apply font-bold text-3xl text-dark;
  }
  `],
})
export class DatepickerHeaderComponent<D> implements OnDestroy {
  private _destroyed = new Subject<void>();
  prevIsDisabled = true;
  nextIsDisabled = false;

  constructor(
    private _calendar: MatCalendar<D>,
    private _dateAdapter: DateAdapter<D>,
    @Inject(MAT_DATE_FORMATS) private _dateFormats: MatDateFormats,
    cdr: ChangeDetectorRef,
  ) {
    _calendar.stateChanges
      .pipe(takeUntil(this._destroyed))
      .subscribe(() => cdr.markForCheck());
      // console.log(this.prevIsDisabled);
      // console.log(this.nextIsDisabled);
  }

  ngOnDestroy() {
    this._destroyed.next();
    this._destroyed.complete();
  }

  get periodLabel() {
    return this._dateAdapter
      .format(
        this._calendar.activeDate,
        this._dateFormats.display.monthYearLabel,
      )
      .toLocaleUpperCase();
  }

  // get prevIsDisabled(): boolean {
  //   return this._dateAdapter.getMonth(this._calendar.activeDate) === this._dateAdapter.getMonth(this._calendar.minDate!);
  // }
  
  // get nextIsDisabled(): boolean {
  //   return this._dateAdapter.getMonth(this._calendar.activeDate) === this._dateAdapter.getMonth(this._calendar.maxDate!);
  // }

  prevClicked() {
    this._calendar.activeDate = this._dateAdapter.addCalendarMonths(this._calendar.activeDate, -1);
    if (this._dateAdapter.getMonth(this._calendar.activeDate) === this._dateAdapter.getMonth(this._calendar.minDate!)) {
      this.prevIsDisabled = true;
    }

    if (this.nextIsDisabled) {
      this.nextIsDisabled = false;
    }
  }

  nextClicked() {
    this._calendar.activeDate = this._dateAdapter.addCalendarMonths(this._calendar.activeDate, 1);
    if (this._dateAdapter.getMonth(this._calendar.activeDate) === this._dateAdapter.getMonth(this._calendar.maxDate!)) {
      this.nextIsDisabled = true;
    }

    if (this.prevIsDisabled) {
      this.prevIsDisabled = false;
    }
  }
}
