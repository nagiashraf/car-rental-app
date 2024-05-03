/**
 * Represents a location.
 */
export interface LocationSearchResult {
  /**
   * The ID of the location.
   */
  id: string;

  /**
   * The ID of the language of the location.
   */
  languageId: number;

  /**
   * The name of the location.
   */
  name: string;

  /**
   * The city of the location.
   */
  city: string;

  /**
   * The country of the location.
   */
  country: string;
}
