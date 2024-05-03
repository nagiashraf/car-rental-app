import { DialogRef } from "@angular/cdk/dialog";
import { CdkOption, ListboxValueChangeEvent } from "@angular/cdk/listbox";
import {
  Component,
  OnDestroy,
  OnInit,
  QueryList,
  ViewChildren,
} from "@angular/core";
import { Subscription, combineLatest } from "rxjs";
import { Currency } from "src/app/core/models/currency.model";
import { LocaleService } from "src/app/core/services/locale.service";

/**
 * Component for displaying a dialog to select a preferred currency.
 * This component uses Angular Material CDK dialog and listbox.
 */
@Component({
  selector: "app-currency-dialog",
  templateUrl: "./currency-dialog.component.html",
  styleUrls: ["./currency-dialog.component.scss"],
  host: { class: "rounded-scrollbar" },
})
export class CurrencyDialogComponent implements OnInit, OnDestroy {
  currencies!: Currency[];
  currentlySelectedCurrency!: Currency;
  combinedSubscription!: Subscription;
  @ViewChildren(CdkOption)
  options!: QueryList<CdkOption>;

  constructor(
    private dialogRef: DialogRef,
    private localeService: LocaleService,
  ) {}

  /**
   * Lifecycle hook called after component initialization.
   * Subscribes to changes in selected currency and list of currencies and focuses on the active option.
   */
  ngOnInit(): void {
    this.combinedSubscription = combineLatest([
      this.localeService.currentlySelectedCurrency,
      this.localeService.getCurrencies(),
    ]).subscribe(([selectedCurrency, currencies]) => {
      this.currentlySelectedCurrency = selectedCurrency;
      if (this.currencies !== currencies) {
        this.currencies = currencies;
        setTimeout(() => {
          this.focusActiveOption();
        });
      }
    });
  }

  /**
   * Lifecycle hook called before component destruction.
   * Unsubscribes from subscriptions to prevent memory leaks.
   */
  ngOnDestroy(): void {
    this.combinedSubscription?.unsubscribe();
  }

  /**
   * Event handler for selecting an option in the listbox.
   * Updates the currently selected currency and closes the dialog.
   * @param event ListboxValueChangeEvent containing the selected currency.
   */
  onOptionSelected({ value: value }: ListboxValueChangeEvent<Currency>): void {
    if (value[0]) {
      // undefined is emitted in case the same value is selected
      this.localeService.currentlySelectedCurrency = value[0];
    }
    this.closeDialog();
  }

  /**
   * Comparator function for comparing currencies by ID. This function is required by the CDK listbox since an object is used as an option value.
   * @param currency1 First currency object.
   * @param currency2 Second currency object.
   * @returns True if currencies have the same ID, false otherwise.
   */
  compareCurrency(currency1: Currency, currency2: Currency): boolean {
    return currency1.id === currency2.id;
  }

  /**
   * Closes the dialog.
   */
  closeDialog(): void {
    this.dialogRef.close();
  }

  /**
   * Focuses on the active option in the listbox.
   */
  focusActiveOption(): void {
    const activeOption = this.options.find((option) => option.isSelected());
    activeOption?.focus();
  }
}
