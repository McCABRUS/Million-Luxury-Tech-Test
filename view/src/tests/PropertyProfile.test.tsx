import { render, screen, within } from '@testing-library/react';
import { describe, it, expect } from 'vitest';
import { PropertyProfile } from '../components/PropertyProfile';
import type { PropertyDetailDto } from '../api/properties';

const sample: PropertyDetailDto = {
  idProperty: 'P1',
  name: 'Casa Bonita',
  address: 'Calle Falsa 123',
  price: 350000,
  images: [],
  traces: [
    { idPropertyTrace: 't1', name: 'Inspection', dateSale: '2022-01-01', tax: 1000 },
    { idPropertyTrace: 't2', name: 'Offer', dateSale: '2022-02-10', tax: 2000 },
  ],
  owners: [],
};

const fmtPrice = (v?: number) =>
    v == null ? 'Price N/A' : new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(v);

describe('PropertyProfile', () => {
  it('renders name, formatted price and address', () => {
    render(<PropertyProfile property={sample} />);
    expect(screen.getByText('Casa Bonita')).toBeInTheDocument();
    expect(screen.getByText(/\$350,000\.00/)).toBeInTheDocument();
    expect(screen.getByText(/Calle Falsa 123/)).toBeInTheDocument();
  });

  it('renders traces with Name Date Sale and Tax fields per trace item', () => {
    render(<PropertyProfile property={sample} />);
    const items = screen.getAllByRole('listitem');
    expect(items.length).toBe(sample.traces.length);
    const first = within(items[0]);
    expect(first.getByText(/Name/i)).toBeInTheDocument();
    expect(first.getByText('Inspection')).toBeInTheDocument();
    expect(first.getByText(/Date Sale/i)).toBeInTheDocument();
    expect(first.getByText('2022-01-01')).toBeInTheDocument();
    expect(first.getByText(/Tax/i)).toBeInTheDocument();
    expect(first.getByText(fmtPrice(1000))).toBeInTheDocument();

    const second = within(items[1]);
    expect(second.getByText(/Name/i)).toBeInTheDocument();
    expect(second.getByText('Offer')).toBeInTheDocument();
    expect(second.getByText(/Date Sale/i)).toBeInTheDocument();
    expect(second.getByText('2022-02-10')).toBeInTheDocument();
    expect(second.getByText(/Tax/i)).toBeInTheDocument();
    expect(second.getByText(fmtPrice(2000))).toBeInTheDocument();
  });
});