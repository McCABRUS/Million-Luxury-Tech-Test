// src/pages/PropertyDetailPage.tsx
import { useParams, useNavigate } from 'react-router-dom';
import { PropertyGallery } from '../components/PropertyGallery';
import { PropertyProfile } from '../components/PropertyProfile';
import { OwnerProfile } from '../components/OwnerProfile';
import { useProperty } from '../hooks/useProperty';
import '../styles/PropertyDetailPage.css';

export default function PropertyDetailPage({ id: propId }: { id?: string }) {
  const params = useParams<{ id?: string }>();
  const navigate = useNavigate();
  const id = propId ?? params.id;

  const { data: property, loading, error } = useProperty(id);

  if (!id) return <div className="pd-page--center">Property id is missing</div>;
  if (loading) return <div className="pd-page--center">Loading property...</div>;
  if (error) return <div className="pd-page--center error">Error: {String(error)}</div>;
  if (!property) return <div className="pd-page--center">Not found</div>;

  return (
    <main className="pd-page">
      <div className="pd-header">
        <button
          type="button"
          className="pd-back-btn"
          onClick={() => navigate(-1)}
          aria-label="Go back"
        >
          ← Back
        </button>

        <div className="pd-title-block">
          <h1 className="pd-title-lg">{property.name ?? 'Property'}</h1>
          <div className="pd-price-lg">
            {property.price == null
              ? 'Price N/A'
              : new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(
                  property.price,
                )}
          </div>
        </div>
      </div>

      <div className="pd-gallery-full">
        <PropertyGallery images={property.images ?? []} />
      </div>

      <div className="pd-content">
        <section className="pd-right">
          <PropertyProfile property={property} />
          <OwnerProfile owners={property.owners ?? []} />
        </section>
      </div>
    </main>
  );
}