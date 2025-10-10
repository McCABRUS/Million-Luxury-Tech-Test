import Filters from '../components/Filters';
import PropertyCard from '../components/PropertyCard';
import { useProperties } from '../hooks/useProperties';

export default function PropertyListPage() {
  const { data, loading, error, params, setParams } = useProperties({ page: 1, pageSize: 20 });
  const handleChange = (partial: any) => setParams((prev: any) => ({ ...prev, ...partial, page: 1 }));

  return (
    <div className="app-container">
      <header className="app-header">
        <h1>Properties</h1>
      </header>

      <Filters {...params} onChange={handleChange} />

      {loading && <p>Loading...</p>}
      {error && <p className="empty">{error}</p>}

      <section className="grid">
        {data.length === 0 ? <div className="empty">No properties found</div> : data.map(p => <PropertyCard key={p.idProperty} prop={p} />)}
      </section>
    </div>
  );
}