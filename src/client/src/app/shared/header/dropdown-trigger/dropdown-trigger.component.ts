import { Component, EventEmitter, Output } from "@angular/core";
import { fade } from "../animations";

/**
 * Represents a dropdown trigger component.
 * Displays a button that, when clicked, toggles the visibility of a dropdown panel.
 */
@Component({
  selector: "app-dropdown-trigger",
  templateUrl: "./dropdown-trigger.component.html",
  styleUrls: ["./dropdown-trigger.component.scss"],
  animations: [fade],
})
export class DropdownTriggerComponent {
  /**
   * Flag to track the state of the dropdown.
   */
  isOpen = false;

  /**
   * Event emitted when the dropdown panel is opened.
   */
  @Output() dropdownOpened = new EventEmitter<void>();

  /**
   * Event emitted when the dropdown panel is closed.
   */
  @Output() dropdownClosed = new EventEmitter<void>();

  /**
   * Toggles the visibility of the dropdown panel.
   */
  toggleDropdown(): void {
    this.isOpen = !this.isOpen;
  }

  /**
   * Closes the dropdown panel.
   */
  closeDropdown(): void {
    this.isOpen = false;
  }
}
