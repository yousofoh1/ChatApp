// const primeui = require("tailwindcss-primeui");
import PrimeUI from "tailwindcss-primeui";

module.exports = {
  content: [
    "./src/**/*.{html,ts,scss}", // Angular-specific paths
  ],
  theme: {
    extend: {},
  },
  plugins: [
    // primeui(), // ✅ enable the PrimeUI Tailwind plugin
    PrimeUI,
  ],
};
