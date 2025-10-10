import { Link } from 'react-router-dom';
import type { PropertyListDto } from '../api/properties';

type Props = { prop: PropertyListDto };

export default function PropertyCard({ prop }: Props) {
  const imgSrc = prop.image ?? '/placeholder.png';
  return (
    <article className="card" aria-label={prop.name}>
      <Link to={`/properties/${encodeURIComponent(prop.idProperty)}`} style={{ textDecoration: 'none', color: 'inherit' }}>
        <img className="thumb" src={imgSrc} alt={prop.name} />
      </Link>

      <div className="body">
        <Link to={`/properties/${encodeURIComponent(prop.idProperty)}`} style={{ textDecoration: 'none', color: 'inherit' }}>
          <h3 className="title">{prop.name}</h3>
        </Link>
        <div className="meta">{prop.address}</div>

        <div className="row">
          <div className="price">${prop.price.toLocaleString()}</div>
          <Link className="btn" to={`/properties/${encodeURIComponent(prop.idProperty)}`}>More Details</Link>
        </div>
      </div>
    </article>
  );
}