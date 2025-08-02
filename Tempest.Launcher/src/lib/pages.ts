export const mainPages: { label: string, path: string }[] = [
    { label: "Home", path: "/" },
    { label: "Library", path: "/library" },
    { label: "Servers", path: "/servers" },
    { label: "Multiplayer", path: "/multiplayer" }
];

export const toReadablePathSegment = (segment: string) =>
	segment
		.split("-")
		.map(word => word.charAt(0).toUpperCase() + word.slice(1))
		.join(" ");
