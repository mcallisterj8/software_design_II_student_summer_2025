using BugLab01.Data;
using Microsoft.EntityFrameworkCore;
using BugLab01.Models;

namespace BugLab01.Services;

public class MusicQueryService {
    private readonly ApplicationDbContext _context;

    public MusicQueryService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<Artist>> GetAllArtistsWithAlbums() {
        return await _context.Artist
            .Include(artist => artist.Albums)
            .Where(artist => artist.Albums.Count > 0)
            .ToListAsync();
    }

    public async Task<List<Artist>> GetAllArtistsWithMoreThanOneAlbum() {
        return await _context.Artist
            .Include(artist => artist.Albums)
            .Where(a => a.Albums.Count > 1)
            .ToListAsync();
    }

    public async Task<Artist?> GetArtistByNameWithAlbums(string artistName) {
        return await _context.Artist
            .Include(artist => artist.Albums)
            .FirstOrDefaultAsync(artist => artist.Name == artistName);
    }

    public async Task<List<Track>> GetTracksByAlbumId(int albumId) {
        return await _context.Track
            .Where(track => track.AlbumId == albumId)
            .ToListAsync();
    }

    public async Task<List<Genre>> GetAllGenresWithTracks() {
        return await _context.Genre
            .Include(genre => genre.Tracks)
            .ToListAsync();
    }

    public async Task<List<Track>> GetTracksByGenreId(int genreId) {
        return await _context.Track
            .Where(track => track.GenreId == genreId)
            .ToListAsync();
    }

    public async Task<List<Statistic>> GetTotalTracksByAlbum() {
        return await _context.Album
            .Select(album => new Statistic {
                Label = album.Title,
                Value = album.Tracks.Count
            })
            .ToListAsync();

    }

    public async Task<List<Album>> GetAlbumsByArtistId(int artistId) {
        return await _context.Album
            .Where(album => album.ArtistId == artistId)
            .ToListAsync();
    }

    public async Task<List<Playlist>> GetAllPlaylistsWithTracks() {
        return await _context.Playlist
            .Include(playlist => playlist.Tracks)
            .Where(playlist => playlist.Tracks.Count > 0)
            .ToListAsync();
    }

    public async Task<List<Statistic>> GetAverageDurationByGenre() {
        return await _context.Genre
            .Select(genre => new Statistic {
                Label = genre.Name,
                Value = genre.Tracks.Any() ? (decimal)genre.Tracks.Average(track => track.Milliseconds) / 1000 : 0,
                ValueMetric = "Seconds"

            })
            .ToListAsync();
    }

    public async Task<List<Artist>> GetArtistsWithoutAlbums() {
        return await _context.Artist
            .Where(artist => artist.Albums.Count == 0)
            .ToListAsync();
    }

    public async Task<List<Track>> GetTracksWithGenreAndAlbum() {
        return await _context.Track
            .Include(track => track.Genre)
            .Include(track => track.Album)
            .ToListAsync();
    }

    public async Task<List<TrackDetails>> GetTrackDetails() {
        return await _context.Track
            .Select(t => new TrackDetails {
                Track = t.Name,
                Album = t.Album.Title,
                Artist = t.Album.Artist.Name
            })
            .ToListAsync();
    }

    public async Task<List<Statistic>> GetAlbumsWithTrackDuration() {
        return await _context.Album
            .Select(album => new Statistic {
                Label = album.Title,
                Value = album.Tracks.Sum(t => t.Milliseconds) / 1000,
                ValueMetric = "Seconds"
            })
            .ToListAsync();
    }

    public async Task<List<Statistic>> GetGenreTrackCounts() {
        return await _context.Genre
            .Select(genre => new Statistic {
                Label = genre.Name,
                Value = genre.Tracks.Count,
            })
            .ToListAsync();
    }

    public async Task<List<Statistic>> GetPlaylistsWithTrackCount() {
        return await _context.Playlist
            .Select(playlist => new Statistic {
                Label = playlist.Name,
                Value = playlist.Tracks.Count()
            })
            .ToListAsync();
    }

    public async Task<List<Track>> GetTracksByPlaylistId(int playlistId) {
        return await _context.Playlist
            .Where(playlist => playlist.PlaylistId == playlistId)
            .SelectMany(playlist => playlist.Tracks)
            .ToListAsync();
    }

    public async Task<Playlist?> GetPlaylistWithMostTracks() {
        return await _context.Playlist
            .OrderByDescending(playlist => playlist.Tracks.Count)
            .FirstOrDefaultAsync();
    }

    public async Task<Playlist?> GetPlaylistWithLeastTracks() {
        return await _context.Playlist
            .OrderBy(playlist => playlist.Tracks.Count)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Statistic>> GetTopFivePlaylistsWithMostTracks() {
        return await _context.Playlist
            .OrderByDescending(playlist => playlist.Tracks.Count)
            .Select(playlist => new Statistic {
                Label = playlist.Name,
                Value = playlist.Tracks.Count
            })
            .Take(5)
            .ToListAsync();
    }

    public async Task<List<Statistic>> GetBottomFivePlaylistsWithLeastTracks() {
        return await _context.Playlist
            .OrderBy(playlist => playlist.Tracks.Count)
            .Select(playlist => new Statistic {
                Label = playlist.Name,
                Value = playlist.Tracks.Count
            })
            .Take(5)
            .ToListAsync();
    }
}
