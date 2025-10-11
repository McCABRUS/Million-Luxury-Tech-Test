import '@testing-library/jest-dom';

const orig = console.error;
console.error = (...args: any[]) => {
  orig.apply(console, args);
};