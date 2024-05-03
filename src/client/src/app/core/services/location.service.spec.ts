import { TestBed } from "@angular/core/testing";
import { LocationService } from "./location.service";
import { LocaleService } from "./locale.service";
import { HttpClient } from "@angular/common/http";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";

describe("LocationService", () => {
  let locationService: LocationService;
  let localeServiceSpy: jasmine.SpyObj<LocaleService>;
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    const localeSpy = jasmine.createSpyObj("LocaleService", [
      "currentlySelectedLanguage",
    ]);
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        LocationService,
        { provide: LocaleService, useValue: localeServiceSpy },
      ],
    });
    locationService = TestBed.inject(LocationService);
    localeServiceSpy = TestBed.inject(
      LocaleService,
    ) as jasmine.SpyObj<LocaleService>;
    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it("should be created", () => {
    expect(locationService).toBeTruthy();
  });
});
