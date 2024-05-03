import { ComponentFixture, TestBed } from "@angular/core/testing";
import { DropdownTriggerComponent } from "./dropdown-trigger.component";
import { OverlayModule } from "@angular/cdk/overlay";
import { Component, DebugElement } from "@angular/core";
import { By } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { click } from "src/testing-utilities";

@Component({
  template: `
    <app-dropdown-trigger
      (dropdownOpened)="onDropdownOpened()"
      (dropdownClosed)="onDropdownClosed()"
    >
      <div class="display-value">Dropdown display value</div>
      <div class="dropdown-panel">Dropdown panel content</div>
    </app-dropdown-trigger>
  `,
})
class TestHostComponent {
  dropdownOpenedEmitted = false;
  dropdownClosedEmitted = false;

  onDropdownOpened() {
    this.dropdownOpenedEmitted = true;
  }
  onDropdownClosed() {
    this.dropdownClosedEmitted = true;
  }
}

describe("DropdownComponent", () => {
  let testHostComponent: TestHostComponent;
  let fixture: ComponentFixture<TestHostComponent>;
  let dropdownTriggerComponentDe: DebugElement;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DropdownTriggerComponent, TestHostComponent],
      imports: [BrowserAnimationsModule, OverlayModule],
    });
    fixture = TestBed.createComponent(TestHostComponent);
    testHostComponent = fixture.componentInstance;
    dropdownTriggerComponentDe = fixture.debugElement.query(
      By.directive(DropdownTriggerComponent),
    );
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(dropdownTriggerComponentDe).toBeTruthy();
  });

  it("should initially be closed", () => {
    const dropdownTriggerComponent =
      dropdownTriggerComponentDe.componentInstance;
    expect(dropdownTriggerComponent.isOpen).toBeFalse();
  });

  it("should emit dropdownOpened when opened", () => {
    const dropdownButton = dropdownTriggerComponentDe.query(By.css("button"));
    click(dropdownButton);
    fixture.detectChanges();
    expect(testHostComponent.dropdownOpenedEmitted).toBeTrue();
  });

  it("should emit dropdownClosed when closed", () => {
    const dropdownButton = dropdownTriggerComponentDe.query(By.css("button"));
    click(dropdownButton);
    fixture.detectChanges();
    click(dropdownButton);
    fixture.detectChanges();
    expect(testHostComponent.dropdownClosedEmitted).toBeTrue();
  });

  it("should toggle dropdown open/close", () => {
    const dropdownButton = dropdownTriggerComponentDe.query(By.css("button"));
    const dropdownTriggerComponent =
      dropdownTriggerComponentDe.componentInstance as DropdownTriggerComponent;

    click(dropdownButton);
    expect(dropdownTriggerComponent.isOpen).toBeTrue();

    click(dropdownButton);
    expect(dropdownTriggerComponent.isOpen).toBeFalse();
  });

  it("should close dropdown when backdrop is clicked", () => {
    const dropdownButton = dropdownTriggerComponentDe.query(By.css("button"));
    const dropdownTriggerComponent =
      dropdownTriggerComponentDe.componentInstance as DropdownTriggerComponent;

    click(dropdownButton);
    fixture.detectChanges();
    expect(dropdownTriggerComponent.isOpen).toBeTrue();

    const backdrop = document.querySelector(".cdk-overlay-backdrop");
    backdrop?.dispatchEvent(new Event("click"));
    fixture.detectChanges();
    expect(dropdownTriggerComponent.isOpen).toBeFalse();
  });
});
