import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from "@angular/core/testing";
import { CurrencyDialogComponent } from "./currency-dialog.component";
import { LocaleService } from "src/app/core/services/locale.service";
import { DialogRef } from "@angular/cdk/dialog";
import { of } from "rxjs";
import { Currency } from "src/app/core/models/currency.model";
import { asyncData, click } from "src/testing-utilities";
import { By } from "@angular/platform-browser";
import { CdkListboxModule, CdkOption } from "@angular/cdk/listbox";
import { QueryList } from "@angular/core";

const mockCurrencies: Currency[] = [
  { id: 1, code: "USD", name: "US Dollar" },
  { id: 2, code: "EUR", name: "Euro" },
];
const mockSelectedCurrency: Currency = mockCurrencies[0];

describe("CurrencyDialogComponent", () => {
  let component: CurrencyDialogComponent;
  let fixture: ComponentFixture<CurrencyDialogComponent>;
  let localeServiceSpy: jasmine.SpyObj<LocaleService>;
  let dialogRefServiceSpy: jasmine.SpyObj<DialogRef>;

  beforeEach(() => {
    localeServiceSpy = jasmine.createSpyObj(
      "LocaleService",
      ["getCurrencies"],
      {
        currentlySelectedCurrency: of(mockSelectedCurrency),
      },
    );
    dialogRefServiceSpy = jasmine.createSpyObj("DialogRef", ["close"]);

    localeServiceSpy.getCurrencies.and.returnValue(asyncData(mockCurrencies));

    TestBed.configureTestingModule({
      declarations: [CurrencyDialogComponent],
      providers: [
        { provide: LocaleService, useValue: localeServiceSpy },
        { provide: DialogRef, useValue: dialogRefServiceSpy },
      ],
      imports: [CdkListboxModule],
    });
    fixture = TestBed.createComponent(CurrencyDialogComponent);
    component = fixture.componentInstance;
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should close dialog when close button is clicked", () => {
    const closeButton = fixture.nativeElement.querySelector("button");
    click(closeButton);
    expect(dialogRefServiceSpy.close).toHaveBeenCalled();
  });

  it("should render languages correctly", fakeAsync(() => {
    fixture.detectChanges();
    tick();
    fixture.detectChanges();
    const currencyElements = fixture.debugElement.queryAll(
      By.css("li p:first-child"),
    );
    expect(currencyElements.length).toBe(mockCurrencies.length);
    expect(currencyElements[0].nativeElement.textContent.trim()).toBe(
      mockCurrencies[0].name,
    );
    expect(currencyElements[1].nativeElement.textContent.trim()).toBe(
      mockCurrencies[1].name,
    );
  }));

  it("should select the current currency", fakeAsync(() => {
    fixture.detectChanges();
    tick();
    fixture.detectChanges();
    const selectedOptionValue = component.options.find((option) =>
      option.isSelected(),
    )?.value as Currency;
    expect(selectedOptionValue.id).toEqual(mockSelectedCurrency.id);
  }));

  it("should compare currencies correctly", () => {
    expect(
      component.compareCurrency(mockCurrencies[0], mockCurrencies[0]),
    ).toBeTrue();
    expect(
      component.compareCurrency(mockCurrencies[0], mockCurrencies[1]),
    ).toBeFalse();
  });

  it("should update selected currency and close dialog when option is selected", fakeAsync(() => {
    fixture.detectChanges();
    tick();
    fixture.detectChanges();
    const lastOption = fixture.nativeElement.querySelectorAll(
      ".cdk-option",
    )[1] as HTMLElement;
    click(lastOption);
    expect(
      Object.getOwnPropertyDescriptor(
        localeServiceSpy,
        "currentlySelectedCurrency",
      )?.set,
    ).toHaveBeenCalledWith(mockCurrencies[1]);
    expect(dialogRefServiceSpy.close).toHaveBeenCalled();
  }));

  it("should focus active option on component initialization", fakeAsync(() => {
    const option1: jasmine.SpyObj<CdkOption> = jasmine.createSpyObj(
      "CdkOption",
      ["isSelected", "focus"],
    );
    option1.isSelected.and.returnValue(false);
    const option2: jasmine.SpyObj<CdkOption> = jasmine.createSpyObj(
      "CdkOption",
      ["isSelected", "focus"],
    );
    option2.isSelected.and.returnValue(true);

    fixture.detectChanges();
    component.options = new QueryList<CdkOption>();
    component.options.reset([option1, option2]);
    tick();
    fixture.detectChanges();

    expect(option1.focus).not.toHaveBeenCalled();
    expect(option2.focus).toHaveBeenCalled();
  }));
});
