import { useTranslation } from "react-i18next";

type Props = {
  analytics: {
    avgTemperature: number;
    avgHumidity: number;
    avgCO2: number;
    avgAirQuality: number;
    activeSensors: number;
    offlineSensors: number;
  } | null;
};

export default function ShelterAnalyticsCards({ analytics }: Props) {
  const { t } = useTranslation();

  if (!analytics) return null;

  const cards = [
    {
      title: t("avgTemperature"),
      value: `${analytics.avgTemperature.toFixed(1)} °C`,
    },
    {
      title: t("avgHumidity"),
      value: `${analytics.avgHumidity.toFixed(1)} %`,
    },
    {
      title: t("avgCO2"),
      value: `${analytics.avgCO2.toFixed(0)} ppm`,
    },
    {
      title: t("avgAirQuality"),
      value: `${analytics.avgAirQuality.toFixed(1)} %`,
    },
    {
      title: t("activeSensors"),
      value: analytics.activeSensors,
    },
    {
      title: t("offlineSensors"),
      value: analytics.offlineSensors,
    },
  ];

  return (
    <div
      className="
        grid
        grid-cols-1
        md:grid-cols-2
        xl:grid-cols-3
        gap-4
      "
    >
      {cards.map((card) => (
        <div
          key={card.title}
          className="
            bg-[#354F52]
            border border-[#52796F]/30
            rounded-xl
            p-5
            shadow-lg
          "
        >
          <div className="text-sm text-gray-400 mb-2">{card.title}</div>

          <div className="text-3xl font-bold text-white">{card.value}</div>
        </div>
      ))}
    </div>
  );
}
