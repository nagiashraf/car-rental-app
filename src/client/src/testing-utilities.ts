import { DebugElement } from "@angular/core";
import { defer } from "rxjs";

export function asyncData<T>(data: T) {
  return defer(() => Promise.resolve(data));
}

export function click(el: DebugElement | HTMLElement): void {
  if (el instanceof HTMLElement) {
    el.click();
  } else {
    el.triggerEventHandler("click");
  }
}
