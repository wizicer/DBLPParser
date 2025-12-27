namespace DblpCli.Models;

using System.Linq;

public class PublisherPrefixs
{
    public static string[] GetAllDbPublisherPrefixes()
    {
        var dbaclass = new[] {
            "journals/tods/",
            "journals/tois/",
            "journals/tkde/",
            "journals/vldb/",
            "journals/pvldb/",
            "conf/sigmod/",
            "conf/kdd/",
            "conf/icde/",
            "conf/sigir/",
            "conf/vldb/",
        };
        var dbbclass = new[] {
            "journals/tkdd/", "journals/tweb/", "journals/aei/", "journals/dke/", "journals/datamine/", "journals/ejis/", "journals/geoinformatica/", "journals/ipm/", "journals/isci/", "journals/is/", "journals/jasis/", "journals/ws/", "journals/kais/", "conf/cikm/", "conf/wsdm/", "conf/pods/", "conf/dasfaa/", "conf/ecml/", "conf/semweb/", "conf/icdm/", "conf/icdt/", "conf/edbt/", "conf/cidr/", "conf/sdm/"
        };
        var dbcclass = new[] {
            "journals/dpd/", "journals/iam/", "journals/ipl/", "journals/ir/", "journals/ijcis/", "journals/gis/", "journals/ijis/", "journals/ijkm/", "journals/ijswis/", "journals/jcis/", "journals/jdm/", "journals/jiis/", "journals/jsis/", "conf/apweb/", "conf/dexa/", "conf/ecir/", "conf/esws/", "conf/webdb/", "conf/er/", "conf/mdm/", "conf/ssdbm/", "conf/waim/", "conf/ssd/", "conf/pakdd/", "conf/wise/"
        };
        var otherclass = new string[] {
            "journals/pvldb/",
        };

        return new[] { dbaclass, dbbclass, dbcclass, otherclass }.SelectMany(_ => _).ToArray();
    }
}
