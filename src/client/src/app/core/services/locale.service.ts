import { Inject, Injectable, LOCALE_ID } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { Language } from "../models/language.model";
import { Currency } from "../models/currency.model";

@Injectable({
  providedIn: "root",
})
export class LocaleService {
  // TODO: When deploying, get current currency from cookie
  private currentlySelectedCurrencySubject = new BehaviorSubject<Currency>({
    id: 25,
    code: "USD",
    name: "United States dollar",
  });
  currentlySelectedLanguage!: Language;
  baseUrl = environment.apiUrl;
  // TODO: When deploying, get current language from cookie instead of hard-coded array
  private languages = [
    {
      id: 1,
      code: "en-US",
      englishName: "English",
      nativeName: "English",
      flagImagePath: "https://localhost:7298/images/flags/en-us.svg",
      isRtl: false,
    },
    {
      id: 2,
      code: "fr-FR",
      englishName: "French",
      nativeName: "Français",
      flagImagePath: "https://localhost:7032/images/flags/fr-fr.svg",
      isRtl: false,
    },
    {
      id: 3,
      code: "es-ES",
      englishName: "Spanish",
      nativeName: "Español",
      flagImagePath: "https://localhost:7032/images/flags/es-es.svg",
      isRtl: false,
    },
    {
      id: 4,
      code: "ar-SA",
      englishName: "Arabic",
      nativeName: "العربية",
      flagImagePath: "https://localhost:7032/images/flags/ar-sa.svg",
      isRtl: true,
    },
  ];

  constructor(
    @Inject(LOCALE_ID) public locale: string,
    private http: HttpClient,
  ) {
    // TODO: When deploying, get current language from cookie
    this.currentlySelectedLanguage = this.languages.find((l) =>
      l.code.includes(this.locale),
    )!;
  }

  get currentlySelectedCurrency(): Observable<Currency> {
    return this.currentlySelectedCurrencySubject.asObservable();
  }

  set currentlySelectedCurrency(currency: Currency) {
    this.currentlySelectedCurrencySubject.next(currency);
  }

  getLanguages(): Observable<Language[]> {
    return this.http.get<Language[]>(this.baseUrl + "languages");
  }

  getCurrencies(): Observable<Currency[]> {
    return this.http.get<Currency[]>(
      this.baseUrl + "currencies/" + this.currentlySelectedLanguage.id,
    );
  }
}
