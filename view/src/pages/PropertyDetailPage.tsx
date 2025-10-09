import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchPropertyById } from '../api/properties';

export default function PropertyDetailPage() {
  const { id } = useParams();
  const [dto, setDto] = useState<any>(null);
  useEffect(() => { if (id) fetchPropertyById(id).then(setDto).catch(() => setDto(null)); }, [id]);
  if (!dto) return <p>Loading detail...</p>;
  return (
    <main>
      <h1>{dto.name}</h1>
      <p>{dto.address}</p>
      <p>{dto.price?.toLocaleString()}</p>
      <div className="gallery">{(dto.images ?? []).map((s:string,i:number) => <img key={i} src={s} alt="" />)}</div>
      <h2>Traces</h2>
      <table>{(dto.traces ?? []).map((t:any,i:number) => <tr key={i}><td>{t.dateSale ?? t.DateSale}</td><td>{t.name ?? t.Name}</td><td>{t.value ?? t.Value}</td></tr>)}</table>
    </main>
  );
}