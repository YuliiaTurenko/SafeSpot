import { useTranslation } from "react-i18next";

type Props = {
  analytics: any[];
};

export default function ShelterAnalyticsTable({analytics,}: Props) {
  const { t } = useTranslation();
  
    return (
    <div className="overflow-x-auto">
      <table className="w-full text-left bg-[#2F3E46] rounded-xl overflow-hidden">
        <thead className="bg-[#354F52]">
          <tr>
            <th className="p-3">{t("sensorName")}</th>
            <th className="p-3">{t("average")}</th>
            <th className="p-3">{t("min")}</th>
            <th className="p-3">{t("max")}</th>
          </tr>
        </thead>

        <tbody>
          {analytics.map((a, i) => (
            <tr key={i} className="border-t border-gray-700">
              <td className="p-3">{a.sensorType}</td>
              <td className="p-3">{a.averageValue}</td>
              <td className="p-3">{a.minValue}</td>
              <td className="p-3">{a.maxValue}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}