import { CdkOption } from "@angular/cdk/listbox";
import { Component, OnInit, QueryList, ViewChildren } from "@angular/core";
import { Observable } from "rxjs";
import { Language } from "src/app/core/models/language.model";
import { LocaleService } from "src/app/core/services/locale.service";

/**
 * A component that displays a list of languages using Angular's CDK listbox.
 */
@Component({
  selector: "app-languages-panel",
  templateUrl: "./languages-panel.component.html",
  styleUrls: ["./languages-panel.component.scss"],
})
export class LanguagesPanelComponent implements OnInit {
  /** Observable holding the list of languages to be displayed */
  languages$!: Observable<Language[]>;

  /** Currently selected language */
  selectedLanguage: Language = this.localeService.currentlySelectedLanguage;

  /** QueryList of CdkOption elements */
  @ViewChildren(CdkOption)
  options!: QueryList<CdkOption>;

  /**
   * Constructor of LanguagesPanelComponent
   * @param localeService The service that provides language and locale functionalities.
   */
  constructor(private localeService: LocaleService) {}

  /** Initialization lifecycle hook */
  ngOnInit(): void {
    this.languages$ = this.localeService.getLanguages();
  }

  /**
   * Focuses on the active option in the list.
   * Finds the first selected option and focuses on it.
   */
  focusActiveOption(): void {
    this.options.find((option) => option.isSelected())?.focus();
  }
}
