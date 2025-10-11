import { useState } from 'react';
type Props = { images?: string[]; initialIndex?: number };

export function PropertyGallery({ images = [], initialIndex = 0 }: Props) {
  const [index, setIndex] = useState(Math.max(0, Math.min(initialIndex, images.length - 1)));

  if (!images || images.length === 0) {
    return (
      <div className="gallery gallery--empty">
        <div className="gallery-main">
          <div className="gallery-placeholder">No images</div>
        </div>
      </div>
    );
  }

  return (
    <div className="gallery">
      <div className="gallery-main">
        <img
          src={images[index]}
          alt={`Property image ${index + 1}`}
          loading="lazy"
          onError={(e) => { (e.currentTarget as HTMLImageElement).src = '/placeholder.jpg'; }}
        />
      </div>

      <div className="gallery-thumbs" role="tablist" aria-label="Property thumbnails">
        {images.map((src, i) => (
          <button
            key={src + i}
            type="button"
            className={`thumb ${i === index ? 'active' : ''}`}
            onClick={() => setIndex(i)}
            aria-pressed={i === index}
            aria-label={`Show image ${i + 1}`}
          >
            <img
              src={src}
              alt={`Thumbnail ${i + 1}`}
              loading="lazy"
              onError={(e) => { (e.currentTarget as HTMLImageElement).src = '/thumb-placeholder.jpg'; }}
            />
          </button>
        ))}
      </div>
    </div>
  );
}

