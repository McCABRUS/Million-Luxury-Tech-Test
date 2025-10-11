type Props = {
  name?: string;
  address?: string;
  priceFrom?: number;
  priceTo?: number;
  onChange: (p: Partial<{ name: string; address: string; priceFrom?: number; priceTo?: number }>) => void;
};

export default function Filters({ name = '', address = '', priceFrom, priceTo, onChange }: Props) {
  return (
    <form onSubmit={e => e.preventDefault()} className="filters">
      <input
        placeholder="Name"
        type="text"
        value={name}
        onChange={e => onChange({ name: e.target.value })}
      />
      <input
        placeholder="Address"
        type="text"
        value={address}
        onChange={e => onChange({ address: e.target.value })}
      />
      <input
        type="number"
        placeholder="Price from"
        value={priceFrom ?? ''}
        onChange={e => onChange({ priceFrom: e.target.value ? Number(e.target.value) : undefined })}
      />
      <input
        type="number"
        placeholder="Price to"
        value={priceTo ?? ''}
        onChange={e => onChange({ priceTo: e.target.value ? Number(e.target.value) : undefined })}
      />
    </form>
  );
}