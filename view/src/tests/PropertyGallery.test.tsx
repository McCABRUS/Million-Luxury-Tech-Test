import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { describe, it, expect } from 'vitest';
import { PropertyGallery } from '../components/PropertyGallery';

const IMAGES = [
  'https://example.com/img1.jpg',
  'https://example.com/img2.jpg',
  'https://example.com/img3.jpg',
];

describe('PropertyGallery', () => {
  it('renders main image and thumbnails', () => {
    render(<PropertyGallery images={IMAGES} initialIndex={0} />);
    const main = screen.getByRole('img', { name: /Property image 1/i });
    expect(main).toBeVisible();
    const thumbs = screen.getAllByRole('button', { name: /Show image/i });
    expect(thumbs.length).toBe(IMAGES.length);
  });

  it('changes main image when a thumbnail is clicked', async () => {
    render(<PropertyGallery images={IMAGES} initialIndex={0} />);
    const user = userEvent.setup();
    const thumb2 = screen.getByRole('button', { name: /Show image 2/i });
    await user.click(thumb2);
    const main = screen.getByRole('img', { name: /Property image 2/i });
    expect(main).toHaveAttribute('src', IMAGES[1]);
  });

  it('shows placeholder when images is empty', () => {
    render(<PropertyGallery images={[]} />);
    expect(screen.getByText(/No images/i)).toBeInTheDocument();
  });
});