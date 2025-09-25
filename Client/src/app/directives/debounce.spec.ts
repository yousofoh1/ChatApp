import { Debounce } from './debounce';

describe('Debounce', () => {
  it('should create an instance', () => {
    const directive = new Debounce();
    expect(directive).toBeTruthy();
  });
});
