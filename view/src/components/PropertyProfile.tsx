import type { PropertyDetailDto } from '../api/properties';

type Props = { property: PropertyDetailDto };

export function PropertyProfile({ property }: Props) {
  const fmtPrice = (v?: number) =>
    v == null ? 'Price N/A' : new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(v);

  return (
    <section className="pp-card">
      <div className="pp-row pp-head">
        <div className="pp-left">
          <h2 className="pp-title">{property.name ?? '—'}</h2>
          <div className="pp-address">{property.address ?? 'Address not provided'}</div>
        </div>
        <div className="pp-right">
          <div className="pp-price">{fmtPrice(property.price)}</div>
        </div>
      </div>

      <div className="pp-row pp-meta-row">
        <div className="pp-meta-item"><strong>Reference</strong><div className="pp-meta-val">{property.idProperty}</div></div>
      </div>

      <div className="pp-traces">
        <h3 className="pp-subtitle">Traces</h3>
        <ul className="pp-trace-list">
          {property.traces?.length ? property.traces.map(t => (
            <li key={t.idPropertyTrace} className="pp-trace-item">
              <div className="pp-trace-flex">
                <div className="pp-trace-field"><span className="pp-trace-label">Name</span><span className="pp-trace-val">{t.name ?? '—'}</span></div>
                <div className="pp-trace-field"><span className="pp-trace-label">Date Sale</span><span className="pp-trace-val">{t.dateSale ?? '—'}</span></div>
                <div className="pp-trace-field"><span className="pp-trace-label">Tax</span><span className="pp-trace-val">{fmtPrice(t.tax) ?? '—'}</span></div>
              </div>
            </li>
          )) : <div className="pp-empty">No traces</div>}
        </ul>
      </div>
    </section>
  );
}