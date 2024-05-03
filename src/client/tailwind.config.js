/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    screens: {
      xs: "576px",
      sm: "768px",
      md: "992px",
      lg: "1200px",
    },
    container: {
      center: true,
      padding: "1rem",
    },
    fontFamily: {
      body: ["Roboto", "sans-serif"],
      heading: ["Raleway", "sans-serif"],
    },
    extend: {
      colors: {
        accent: "#0099FF",
        dark: "#333333",
        "dark-medium": "#666666",
        "dark-light": "#999999",
        "off-white": "#F0F0F0",
        "flash-white": "#F1F2F6",
        error: "#FF0000",
      },
    },
  },
  plugins: [],
};
