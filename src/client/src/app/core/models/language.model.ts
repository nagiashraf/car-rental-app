/**
 * Represents a language with its properties.
 */
export interface Language {
  /**
   * The unique identifier of the language.
   */
  id: number;

  /**
   * The ISO 639-1 language code.
   */
  code: string;

  /**
   * The native name of the language.
   */
  nativeName: string;

  /**
   * The file path to the image of the flag representing the language.
   */
  flagImagePath: string;

  /**
   * Indicates whether the language is written from right to left.
   */
  isRtl: boolean;
}
