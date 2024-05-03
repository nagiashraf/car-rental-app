import { ComponentFixture, TestBed } from "@angular/core/testing";
import { HeaderComponent } from "./header.component";
import { LocaleService } from "src/app/core/services/locale.service";
import { Dialog } from "@angular/cdk/dialog";
import { NO_ERRORS_SCHEMA } from "@angular/core";
import { of } from "rxjs";
import { Language } from "src/app/core/models/language.model";
import { Currency } from "src/app/core/models/currency.model";
import { CurrencyDialogComponent } from "./currency-dialog/currency-dialog.component";
import { click } from "src/testing-utilities";

describe("HeaderComponent", () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;
  let localeServiceSpy: jasmine.SpyObj<LocaleService>;
  let dialogServiceSpy: jasmine.SpyObj<Dialog>;
  const mockLanguage: Partial<Language> = {
    flagImagePath: "testFlagImagePath",
  };
  const mockCurrency: Partial<Currency> = { code: "USD" };

  beforeEach(() => {
    const localeSpy = jasmine.createSpyObj(
      "LocaleService",
      {},
      {
        currentlySelectedLanguage: mockLanguage,
        currentlySelectedCurrency: of(mockCurrency),
      },
    );
    const dialogSpy = jasmine.createSpyObj("Dialog", ["open"]);

    TestBed.configureTestingModule({
      declarations: [HeaderComponent],
      providers: [
        { provide: LocaleService, useValue: localeSpy },
        { provide: Dialog, useValue: dialogSpy },
      ],
      schemas: [NO_ERRORS_SCHEMA],
    });
    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    localeServiceSpy = TestBed.inject(
      LocaleService,
    ) as jasmine.SpyObj<LocaleService>;
    dialogServiceSpy = TestBed.inject(Dialog) as jasmine.SpyObj<Dialog>;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should display currently selected language flag", () => {
    const flagImageEl =
      fixture.nativeElement.querySelector(".display-value img");
    expect(flagImageEl.src)
      .withContext("service returned flagImagePath")
      .toContain(localeServiceSpy.currentlySelectedLanguage?.flagImagePath);
  });

  it("should display currently selected currency code", () => {
    const currencyButton = fixture.nativeElement.querySelector("button");
    expect(currencyButton.textContent.trim())
      .withContext("service returned currency observable")
      .toBe(mockCurrency.code);
  });

  it("should open currency dialog when currency button is clicked", () => {
    const currencyButton = fixture.nativeElement.querySelector("button");
    click(currencyButton);
    expect(dialogServiceSpy.open).toHaveBeenCalledWith(CurrencyDialogComponent);
  });
});
