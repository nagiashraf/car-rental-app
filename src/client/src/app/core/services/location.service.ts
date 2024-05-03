import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { environment } from "src/environments/environment";
import { LocationSearchResult } from "src/app/core/models/location.model";
import { LocaleService } from "./locale.service";

@Injectable({
  providedIn: "root",
})
export class LocationService {
  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private localeService: LocaleService,
  ) {}

  searchLocations(
    term: string,
    resultsNumber: number,
  ): Observable<LocationSearchResult[]> {
    if (!term) {
      return of([]);
    }
    let params = new HttpParams()
      .set("query", term)
      .set("languageId", this.localeService.currentlySelectedLanguage.id)
      .set("resultsNumber", resultsNumber);

    return this.http.get<LocationSearchResult[]>(
      this.baseUrl + "locations/search",
      {
        params,
      },
    );
  }

  getDropoffLocations(
    pickupLocationId: string,
    languageId: number,
  ): Observable<LocationSearchResult[]> {
    let params = new HttpParams().set("languageId", languageId);

    return this.http.get<LocationSearchResult[]>(
      this.baseUrl + "locations/dropoff-locations/" + pickupLocationId,
      {
        params,
      },
    );
  }
}
