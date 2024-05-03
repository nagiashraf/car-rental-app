import {
  Component,
  EventEmitter,
  Input,
  Output,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { fadeAndSlideUp } from "../animations";

@Component({
  selector: "app-input-dropdown",
  template: `
    <ng-template #root>
      <div @fadeAndSlideUp cdkTrapFocus class="panel">
        <ng-content></ng-content>
      </div>
    </ng-template>
  `,
  styleUrls: ["../shared.scss"],
  animations: [fadeAndSlideUp],
})
export class InputDropdownComponent {
  @Input()
  width!: number;
  @Input()
  minWidth!: number;
  @Input()
  displayValue!: (value: any) => string;
  @Output()
  optionSelected: EventEmitter<any> = new EventEmitter<any>();
  @Output()
  focusActiveOption: EventEmitter<void> = new EventEmitter<void>();
  @ViewChild("root")
  templateRef!: TemplateRef<any>;
}
