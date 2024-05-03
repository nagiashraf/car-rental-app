import { animate, style, transition, trigger } from "@angular/animations";

export const fadeAndSlideUp = trigger("fadeAndSlideUp", [
  transition(":enter", [
    style({ opacity: 0, transform: "translateY(0.5rem)" }),
    animate(
      "300ms ease-out",
      style({ opacity: 1, transform: "translateY(0)" }),
    ),
  ]),
  transition(":leave", [animate("200ms ease-in", style({ opacity: 0 }))]),
]);
