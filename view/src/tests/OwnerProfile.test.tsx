import { render, screen, within } from '@testing-library/react';
import { describe, it, expect } from 'vitest';
import { OwnerProfile } from '../components/OwnerProfile';
import type { OwnerDto } from '../api/properties';

const owners: OwnerDto[] = [
  { idOwner: 'o1', name: 'Alice', address: 'Addr 1', photo: '', birthday: '1990-01-01' },
  { idOwner: 'o2', name: 'Bob', address: 'Addr 2', photo: '', birthday: '1985-05-05' },
];

describe('OwnerProfile', () => {
  it('renders owner cards and shows labels with values', () => {
    render(<OwnerProfile owners={owners} />);

    const item1 = screen.getByTestId('owner-item-o1');
    expect(within(item1).getByText(/Name:/i)).toBeInTheDocument();
    expect(within(item1).getByText('Alice')).toBeInTheDocument();
    expect(within(item1).getByText('Addr 1')).toBeInTheDocument();

    const item2 = screen.getByTestId('owner-item-o2');
    expect(within(item1).getByText(/Name:/i)).toBeInTheDocument();
    expect(within(item2).getByText('Bob')).toBeInTheDocument();
    expect(within(item2).getByText('Addr 2')).toBeInTheDocument();
  });

  it('shows "No owners" when list empty', () => {
    render(<OwnerProfile owners={[]} />);
    expect(screen.getByText(/No owners/i)).toBeInTheDocument();
  });
});