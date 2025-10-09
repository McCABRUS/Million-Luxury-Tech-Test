type Props = {
  name?: string; address?: number; priceFrom?: number; priceTo?: number;
  onChange: (p: any) => void;
};
export default function Filters({ name='', address='', priceFrom='', priceTo='', onChange }: Props) {
  return (
    <form onSubmit={e => e.preventDefault()} className="filters">
      <input placeholder="Name" defaultValue={name} onChange={e => onChange({ name: e.target.value })}/>
      <input placeholder="Address" defaultValue={address} onChange={e => onChange({ address: e.target.value })}/>
      <input type="number" placeholder="Price from" defaultValue={priceFrom as any} onChange={e => onChange({ priceFrom: e.target.value ? Number(e.target.value) : undefined })}/>
      <input type="number" placeholder="Price to" defaultValue={priceTo as any} onChange={e => onChange({ priceTo: e.target.value ? Number(e.target.value) : undefined })}/>
    </form>
  );
}