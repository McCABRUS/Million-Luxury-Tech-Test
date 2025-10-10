import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { fetchPropertyById } from '../api/properties';

export default function PropertyDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [dto, setDto] = useState<any | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;
    setLoading(true);
    fetchPropertyById(id)
      .then(d => setDto(d))
      .catch(() => setDto(null))
      .finally(() => setLoading(false));
  }, [id]);

  if (loading) return <div className="app-container"><p>Loading detail...</p></div>;
  if (!dto) return <div className="app-container"><p className="empty">Property not found</p></div>;

  return (
    <div className="app-container">
      <button className="back-btn" onClick={() => navigate(-1)}>← Back</button>

      <div className="profile">
        <div className="left">
          <div className="profile-card">
            <h2>{dto.name}</h2>
            <div className="sub">{dto.address}</div>
            <div style={{ fontWeight: 700, fontSize: '1.1rem' }}>${dto.price?.toLocaleString()}</div>

            <div className="gallery" style={{ marginTop: 12 }}>
              {(dto.images ?? []).length === 0
                ? <img src="/placeholder.png" alt="placeholder" />
                : (dto.images ?? []).map((s:string, i:number) => <img key={i} src={s} alt={`${dto.name} ${i+1}`} />)}
            </div>
          </div>
        </div>

        <div className="right">
          <div className="profile-card">
            <h3>Property profile</h3>
            <p><strong>Code:</strong> {dto.codeInternal ?? dto.CodeInternal}</p>
            <p><strong>Year:</strong> {dto.year ?? dto.Year}</p>
            <p><strong>Owner:</strong> {dto.idOwner ?? dto.IdOwner}</p>

            <h4 style={{ marginTop:12 }}>Traces</h4>
            <table className="table">
              <thead>
                <tr><th>Date</th><th>Name</th><th>Value</th><th>Tax</th></tr>
              </thead>
              <tbody>
                {(dto.traces ?? []).map((t:any,i:number) =>
                  <tr key={i}>
                    <td>{t.dateSale ?? t.DateSale ?? '-'}</td>
                    <td>{t.name ?? t.Name}</td>
                    <td>{t.value ?? t.Value}</td>
                    <td>{t.tax ?? t.Tax}</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}