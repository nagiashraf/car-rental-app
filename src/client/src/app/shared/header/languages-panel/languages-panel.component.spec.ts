import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from "@angular/core/testing";
import { LanguagesPanelComponent } from "./languages-panel.component";
import { LocaleService } from "src/app/core/services/locale.service";
import { defer } from "rxjs";
import { CdkListboxModule, CdkOption } from "@angular/cdk/listbox";
import { QueryList } from "@angular/core";
import { Language } from "src/app/core/models/language.model";
import { By } from "@angular/platform-browser";
import { asyncData } from "src/testing-utilities";

const mockLanguages: Language[] = [
  {
    id: 1,
    nativeName: "English",
    code: "en",
    flagImagePath: "path/to/english-flag",
    isRtl: false,
  },
  {
    id: 2,
    nativeName: "Español",
    code: "es",
    flagImagePath: "path/to/spanish-flag",
    isRtl: false,
  },
];

describe("LanguagesPanelComponent", () => {
  let component: LanguagesPanelComponent;
  let fixture: ComponentFixture<LanguagesPanelComponent>;
  let localeServiceSpy: jasmine.SpyObj<LocaleService>;

  beforeEach(() => {
    localeServiceSpy = jasmine.createSpyObj("LocaleService", ["getLanguages"], {
      currentlySelectedLanguage: mockLanguages[0],
    });
    localeServiceSpy.getLanguages.and.returnValue(asyncData(mockLanguages));

    TestBed.configureTestingModule({
      declarations: [LanguagesPanelComponent],
      imports: [CdkListboxModule],
      providers: [{ provide: LocaleService, useValue: localeServiceSpy }],
    });
    fixture = TestBed.createComponent(LanguagesPanelComponent);
    component = fixture.componentInstance;
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should render languages correctly", fakeAsync(() => {
    fixture.detectChanges();
    tick();
    fixture.detectChanges();
    const languageElements = fixture.debugElement.queryAll(By.css("li"));
    expect(languageElements.length)
      .withContext("Number of displayed languages should match")
      .toBe(mockLanguages.length);
    expect(languageElements[0].nativeElement.textContent).toContain("English");
    expect(languageElements[1].nativeElement.textContent).toContain("Español");
  }));

  it("should select the current language", fakeAsync(() => {
    fixture.detectChanges();
    tick();
    fixture.detectChanges();
    const selectedOptionValue = component.options.find((option) =>
      option.isSelected(),
    )?.value;
    expect(selectedOptionValue).toEqual(
      localeServiceSpy.currentlySelectedLanguage.id,
    );
  }));

  it("should focus active option", () => {
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
    component.options = new QueryList<CdkOption>();
    component.options.reset([option1, option2]);

    component.focusActiveOption();

    expect(option1.focus).not.toHaveBeenCalled();
    expect(option2.focus).toHaveBeenCalled();
  });
});
