import {
  animate,
  state,
  style,
  transition,
  trigger,
} from "@angular/animations";

export const indicatorFlip = trigger("indicatorFlip", [
  state(
    "collapsed",
    style({
      transform: "scaleY(1)",
    }),
  ),
  state(
    "expanded",
    style({
      transform: "scaleY(-1)",
    }),
  ),
  transition("* => *", animate("300ms ease-out")),
]);

export const expand = trigger("expand", [
  state(
    "collapsed, void",
    style({
      opacity: 0,
      height: 0,
    }),
  ),
  state(
    "expanded",
    style({
      opacity: 1,
      height: "*",
    }),
  ),
  transition("* => *", animate("300ms ease-in-out")),
]);
