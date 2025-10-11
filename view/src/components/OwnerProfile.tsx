import type { OwnerDto } from '../api/properties';

export function OwnerProfile({ owners = [] }: { owners?: OwnerDto[] }) {
  if (!owners || owners.length === 0) return <section className="op-card">No owners</section>;

  return (
    <section className="op-card">
      <h3 className="op-title">Owners</h3>
      <ul className="op-list">
        {owners.map(o => (
          <li key={o.idOwner} className="op-item">
            <img className="op-photo" src={o.photo ?? '/owner-placeholder.jpg'} alt={o.name ?? 'owner'} loading="lazy"
              onError={(e) => { (e.currentTarget as HTMLImageElement).src = '/owner-placeholder.jpg'; }} />
            <div className="op-info">
              <div className="op-row"><span className="op-label">Name:</span> <span className="op-val">{o.name ?? '—'}</span></div>
              <div className="op-row"><span className="op-label">Address:</span> <span className="op-val">{o.address ?? '—'}</span></div>
              <div className="op-row"><span className="op-label">Birthday:</span> <span className="op-val">{o.birthday ?? '—'}</span></div>
            </div>
          </li>
        ))}
      </ul>
    </section>
  );
}