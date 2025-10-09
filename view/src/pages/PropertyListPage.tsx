import React from 'react';
import Filters from '../components/Filters';
import PropertyCard from '../components/PropertyCard';
import { useProperties } from '../hooks/useProperties';

export default function PropertyListPage() {
  const { data, loading, error, params, setParams } = useProperties({ page:1, pageSize:20 });
  const handleChange = (partial: any) => setParams(prev => ({ ...prev, ...partial, page: 1 }));
  return (
    <main>
      <h1>Properties</h1>
      <Filters {...params} onChange={handleChange} />
      {loading && <p>Loading...</p>}
      {error && <p>{error}</p>}
      <section className="grid">
        {data.map(p => <PropertyCard key={p.idProperty} prop={p} />)}
      </section>
    </main>
  );
}