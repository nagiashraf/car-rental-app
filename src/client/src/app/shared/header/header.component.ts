import { Component } from "@angular/core";
import { Observable } from "rxjs";
import { Language } from "src/app/core/models/language.model";
import { LocaleService } from "src/app/core/services/locale.service";
import { Dialog } from "@angular/cdk/dialog";
import { CurrencyDialogComponent } from "./currency-dialog/currency-dialog.component";
import { Currency } from "src/app/core/models/currency.model";

/**
 * This component represents the header section of the application.
 * It includes the logo, currency selection, and language selection functionalities.
 */
@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"],
})
export class HeaderComponent {
  /**
   * Represents the currently selected language.
   */
  currentlySelectedLanguage: Language =
    this.localeService.currentlySelectedLanguage;

  /**
   * Observable representing the currently selected currency.
   */
  currentlySelectedCurrency$: Observable<Currency> =
    this.localeService.currentlySelectedCurrency;

  /**
   * Constructs the HeaderComponent.
   * @param localeService Service for managing locale-related data.
   * @param dialog Angular CDK dialog service.
   */
  constructor(
    private localeService: LocaleService,
    private dialog: Dialog,
  ) {}

  /**
   * Opens the currency selection dialog.
   */
  openCurrencyDialog(): void {
    this.dialog.open(CurrencyDialogComponent);
  }
}
